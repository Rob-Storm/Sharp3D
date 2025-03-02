using StbImageSharp;
using OpenTK.Graphics.OpenGL4;

namespace Sharp3D.Graphics
{
    public class Texture
    {
        public readonly int Handle;
        public static TextureMode FilterMode = TextureMode.Nearest;

        public static Dictionary<TextureMode, GL_TextureMode> TextureModeLookup = new Dictionary<TextureMode, GL_TextureMode>()
        {
            { TextureMode.Nearest, new GL_TextureMode {MinFilter = TextureMinFilter.Nearest, MagFilter = TextureMagFilter.Nearest} },
            { TextureMode.Linear, new GL_TextureMode {MinFilter = TextureMinFilter.Linear, MagFilter = TextureMagFilter.Linear} }
        };

        public static Texture LoadFromFile(string path)
        {
            int handle = GL.GenTexture();

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, handle);

            StbImage.stbi_set_flip_vertically_on_load(1);

            using(Stream stream = File.OpenRead(path))
            {
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            }

            TextureModeLookup.TryGetValue(FilterMode, out GL_TextureMode textureFilterMode);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)textureFilterMode.MinFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)textureFilterMode.MagFilter);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return new Texture(handle);
        }

        public Texture(int glHandle)
        {
            Handle = glHandle;
        }

        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public static void SetTextureMode(TextureMode newFilterMode)
        {
            FilterMode = newFilterMode;

            TextureModeLookup.TryGetValue(FilterMode, out GL_TextureMode textureFilterMode);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)textureFilterMode.MinFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)textureFilterMode.MagFilter);
        }
    }

    public struct GL_TextureMode
    {
        public TextureMinFilter MinFilter { get; set; }
        public TextureMagFilter MagFilter { get; set; }

    }

    public enum TextureMode
    {
        Nearest,
        Linear,
    }
}
