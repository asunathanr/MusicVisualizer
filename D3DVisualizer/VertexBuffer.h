#pragma once

#include "stdafx.h"

// Allows hiding of messy details in managing a vertex buffer.
// Inspiration for this class was found at: http://www.chadvernon.com/blog/resources/directx9/dynamic-buffers/
// I took the VertexBuffer class in the provided source and modified it to suit my own needs.
class VertexBuffer
{
public:
    VertexBuffer();
    ~VertexBuffer();

    HRESULT CreateBuffer(LPDIRECT3DDEVICE9, UINT numVertices, DWORD FVF, UINT vertexSize, BOOL dynamic);

    HRESULT SetData(UINT numVertices, void * vertices, DWORD flags = D3DLOCK_DISCARD);

    void Render(LPDIRECT3DDEVICE9 device, UINT numPrimitives, D3DPRIMITIVETYPE primitiveType);

    void Release();

private:
    LPDIRECT3DVERTEXBUFFER9 vb;
    UINT numVertices;
    UINT vertexSize;
    DWORD FVF;
};

