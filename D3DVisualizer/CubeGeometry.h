#pragma once

#include "CustomVertex.h"


struct CubeGeometry
{
public:
    CubeGeometry(D3DVECTOR position, float dimensions);
    CubeGeometry(const CubeGeometry& other);
    ~CubeGeometry();

    D3DVECTOR position;

    CUSTOMVERTEX* vertices;
    short* indices;
};

