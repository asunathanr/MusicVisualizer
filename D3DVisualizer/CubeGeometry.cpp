#include "stdafx.h"
#include "CubeGeometry.h"


/**
    See: http://www.directxtutorial.com/Lesson.aspx?lessonid=9-4-7
*/
CubeGeometry::CubeGeometry(D3DVECTOR position, float dimensions)
{
    float yDim = position.y + dimensions;
    vertices = new CUSTOMVERTEX[36]
    {
        { position.x , position.y, position.z, D3DCOLOR_XRGB(0, 0, 255), },    // vertex 0
        { position.x, position.y, position.z, D3DCOLOR_XRGB(0, 255, 0), },     // vertex 1
        { position.x + dimensions, position.y + dimensions, position.z, D3DCOLOR_XRGB(255, 0, 0), },   // 2
        { position.x, position.y + dimensions, position.z, D3DCOLOR_XRGB(0, 255, 255), },  // 3
        { position.x + dimensions, position.y, position.z, D3DCOLOR_XRGB(0, 0, 255), },     // ...
        { position.x, position.y, position.z, D3DCOLOR_XRGB(255, 0, 0), },
        { position.x + dimensions, position.y + dimensions, position.z, D3DCOLOR_XRGB(0, 255, 0), },
        { position.x, position.y + dimensions, position.z, D3DCOLOR_XRGB(0, 255, 255), },
    };

    indices = new short[36]
    {
        0, 1, 2,    // side 1
        2, 1, 3,
        4, 0, 6,    // side 2
        6, 0, 2,
        7, 5, 6,    // side 3
        6, 5, 4,
        3, 1, 7,    // side 4
        7, 1, 5,
        4, 5, 0,    // side 5
        0, 5, 1,
        3, 7, 2,    // side 6
        2, 7, 6,
    };
}

CubeGeometry::CubeGeometry(const CubeGeometry & other)
{
    vertices = new CUSTOMVERTEX[36];
    indices = new short[36];
    for (int i = 0; i < 36; ++i)
    {
        vertices[i] = other.vertices[i];
        indices[i] = other.indices[i];
    }
    
}

CubeGeometry::~CubeGeometry()
{
    if (vertices) {
        delete vertices;
        vertices = nullptr;
    }

    if (indices)
    {
        delete indices;
        indices = nullptr;
    }
}

