using Sharp3D.Core;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Xml.Linq;

namespace Sharp3D.Graphics
{
    public class Shader : IDisposable
    {
        public int Handle { get; private set; }
        public int RenderHandle { get; private set; }

        private Dictionary<string, int> _uniformLocationCache = new Dictionary<string, int>();

        public Shader(string vertexPath, string fragmentPath)
        {
            vertexPath = File.ReadAllText(vertexPath);
            fragmentPath = File.ReadAllText(fragmentPath);

            CreateShader(vertexPath, fragmentPath);
        }

        public void Dispose()
        {
            GL.DeleteShader(Handle);
        }

        public int CompileShader(string shader, ShaderType type)
        {
            int shaderHandle = GL.CreateShader(type);

            GL.ShaderSource(shaderHandle, shader);
            GL.CompileShader(shaderHandle);

            GL.GetShader(shaderHandle, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(Handle);
                Debug.Log(infoLog, LogLevel.Error);
                return 0;
            }
            else Debug.Log($"{type} compiled successfully!", LogLevel.Info);

            return shaderHandle;
        }

        public void CreateShader(string vertexShader, string fragmentShader)
        {
            Handle = GL.CreateProgram();

            int vertHandle = CompileShader(vertexShader, ShaderType.VertexShader);
            int fragHandle = CompileShader(fragmentShader, ShaderType.FragmentShader);


            GL.AttachShader(Handle, vertHandle);
            GL.AttachShader(Handle, fragHandle);

            GL.LinkProgram(Handle);

            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int linkSuccess);
            if (linkSuccess == 0)
            {
                string infoLog = GL.GetProgramInfoLog(Handle);
                Debug.Log(infoLog, LogLevel.Error);
            }
            else Debug.Log("Shader program linked successfully!", LogLevel.Info);

            GL.DetachShader(Handle, vertHandle);
            GL.DetachShader(Handle, fragHandle);

            GL.DeleteShader(vertHandle);
            GL.DeleteShader(fragHandle);

        }

        public void Bind() => GL.UseProgram(Handle);
        public void Unbind() => GL.UseProgram(0);

        public int GetAttribLocation(string attribName) => GL.GetAttribLocation(Handle, attribName);
        public int GetUniformLocation(string name)
        {
            int location = -1;

            if (_uniformLocationCache.TryGetValue(name, out int value))
            {
                return value;
            }
            else
            {
                _uniformLocationCache.Add(name, GL.GetUniformLocation(Handle, name));
                location = GL.GetUniformLocation(Handle, name);
            }

            return location;
                
        }
        public void SetInt(string name, int value) => GL.Uniform1(GetUniformLocation(name), value);
        public void SetUniformMatrix4(string name, Matrix4 data) => GL.UniformMatrix4(GetUniformLocation(name), true, ref data);
    }

}
