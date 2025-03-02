#version 330 core

out vec4 outputColor;

in vec2 texCoord;
in vec3 Normal;

uniform sampler2D texture0;

void main()
{
    vec3 lightDir = normalize(vec3(1.0, 1.0, 1.0));
    float diff = max(dot(Normal, lightDir), 0.5);

    vec3 color = texture(texture0, texCoord).rgb;
    outputColor = vec4(color * diff, 1.0);
   //outputColor = vec4(1.0, 1.0, 1.0, 1.0);
}