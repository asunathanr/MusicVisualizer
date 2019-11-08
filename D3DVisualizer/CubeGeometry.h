#pragma once

#include "CustomVertex.h"


struct CubeGeometry
{
public:
    CubeGeometry(D3DVECTOR position, float dimensions);
    CubeGeometry(const CubeGeometry& other);
    ~CubeGeometry();

    CUSTOMVERTEX* vertices;

private:
    void AddFrontFace(D3DVECTOR position, float dimensions);
    void AddLeftFace(D3DVECTOR position, float dimensions);
    void AddRightFace(D3DVECTOR position, float dimensions);
    void AddTopFace(D3DVECTOR position, float dimensions);
    void AddBackFace(D3DVECTOR position, float dimensions);
    void AddBottomFace(D3DVECTOR position, float dimensions);
};

