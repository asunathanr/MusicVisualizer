#include "stdafx.h"
#include "CubeGeometry.h"


CubeGeometry::CubeGeometry(D3DVECTOR position, float dimensions)
{
    vertices = new CUSTOMVERTEX[36];
    AddFrontFace(position, dimensions);
    AddBackFace(position, dimensions);
    AddLeftFace(position, dimensions);
    AddRightFace(position, dimensions);
    AddTopFace(position, dimensions);
    AddBottomFace(position, dimensions);
}

CubeGeometry::CubeGeometry(const CubeGeometry & other)
{
    vertices = new CUSTOMVERTEX[36];
    for (int i = 0; i < 36; ++i)
    {
        vertices[i] = other.vertices[i];
    }
}

CubeGeometry::~CubeGeometry()
{
    delete vertices;
}

void CubeGeometry::AddFrontFace(D3DVECTOR position, float dimensions)
{
    // First triangle
    vertices[0] = {position.x, position.y, position.z, 0xff00ff00};
    vertices[1] = {position.x, position.y + dimensions, position.z, 0xff00ff00};
    vertices[2] = {position.x + dimensions, position.y + dimensions, position.z, 0xff00ff00};
    
    // Second triangle
    vertices[3] = {position.x, position.y, position.z, 0xff00ff00};
    vertices[4] = {position.x + dimensions, position.y, position.z, 0xff00ff00};
    vertices[5] = {position.x + dimensions, position.y + dimensions, position.z, 0xff00ff00};
}

void CubeGeometry::AddLeftFace(D3DVECTOR position, float dimensions)
{
    // First triangle
    vertices[12] = {position.x + dimensions, position.y, position.z};
    vertices[13] = {position.x + dimensions, position.y, position.z + dimensions};
    vertices[14] = {position.x + dimensions, position.y + dimensions, position.z + dimensions};
    
    // Second triangle
    vertices[15] = {position.x + dimensions, position.y, position.z};
    vertices[16] = {position.x + dimensions, position.y + dimensions, position.z};
    vertices[17] = {position.x + dimensions, position.y + dimensions, position.z + dimensions};
}

void CubeGeometry::AddRightFace(D3DVECTOR position, float dimensions)
{
    // First triangle
    vertices[6] = {position.x, position.y, position.z, 0xffff00ff};
    vertices[7] = {position.x, position.y, position.z + dimensions, 0xffff00ff};
    vertices[8] = {position.x, position.y + dimensions, position.z, 0xffff00ff};
    
    // Second triangle
    vertices[9] = {position.x, position.y + dimensions, position.z};
    vertices[10] = {position.x, position.y, position.z + dimensions};
    vertices[11] = {position.x, position.y + dimensions, position.z + dimensions};
}

void CubeGeometry::AddTopFace(D3DVECTOR position, float dimensions)
{
    // First triangle
    vertices[18] = {position.x, position.y , position.z, 0xff5500ff};
    vertices[19] = {position.x + dimensions, position.y, position.z, 0xff5500ff};
    vertices[20] = {position.x, position.y, position.z + dimensions, 0xff5500ff};
    
    // Second triangle
    vertices[21] = {position.x + dimensions, position.y, position.z + dimensions, 0xff5500ff};
    vertices[22] = {position.x + dimensions, position.y, position.z + dimensions, 0xff5500ff};
    vertices[23] = {position.x, position.y, position.z, 0xff5500ff};
}

void CubeGeometry::AddBackFace(D3DVECTOR position, float dimensions)
{
    float depth = dimensions;

    // First triangle
    vertices[24] = {position.x, position.y, position.z + depth};
    vertices[25] = {position.x, position.y + dimensions, position.z + depth};
    vertices[26] = {position.x + dimensions, position.y + dimensions, position.z + depth};
    
    // Second triangle
    vertices[27] = {position.x, position.y, position.z + depth};
    vertices[28] = {position.x + dimensions, position.y, position.z + depth};
    vertices[29] = {position.x + dimensions, position.y + dimensions, position.z + depth};
}

void CubeGeometry::AddBottomFace(D3DVECTOR position, float dimensions)
{
    float depth = dimensions;

    vertices[30] = {position.x, position.y + dimensions, position.z, 0xffffffff};
    vertices[31] = {position.x + dimensions, position.y + dimensions, position.z, 0xffffffff};
    vertices[32] = {position.x, position.y + dimensions, position.z + dimensions, 0xffffffff};

    vertices[33] = {position.x + dimensions, position.y + dimensions, position.z + dimensions, 0xffffffff};
    vertices[34] = {position.x + dimensions, position.y + dimensions, position.z, 0xffffffff};
    vertices[35] = {position.x, position.y + dimensions, position.z + dimensions, 0xffffffff};
}

