#version 330 core
layout (location = 0) in vec2 aPosition;

uniform vec2 offset;
uniform float rotation;

void main()
{
    mat2 rot = mat2(cos(rotation), -sin(rotation),
                    sin(rotation),  cos(rotation));
    vec2 pos = rot * aPosition + offset;
    gl_Position = vec4(pos, 0.0, 1.0);
}