
out vec4 color;
void main()
{
    // transform the vertex position
    gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;

    // forward the vertex color
    color = gl_Color;
}