#include "stdafx.h"
#include "VertexBuffer.h"


VertexBuffer::VertexBuffer()
{
    vb = NULL;
    numVertices = 0;
    FVF = 0;
}

void VertexBuffer::Release()
{
    SAFE_RELEASE(vb);
    numVertices = 0;
    FVF = 0;
    vertexSize = 0;
}

VertexBuffer::~VertexBuffer()
{
    Release();
}

HRESULT VertexBuffer::CreateBuffer(LPDIRECT3DDEVICE9 device, UINT numVertices, DWORD FVF, UINT vertexSize, BOOL dynamic)
{
    HRESULT hr = S_OK;
    Release();
    this->numVertices = numVertices;
    this->FVF = FVF;
    this->vertexSize = vertexSize;
    IFC(device->CreateVertexBuffer(numVertices*vertexSize, D3DUSAGE_WRITEONLY | D3DUSAGE_DYNAMIC, FVF, D3DPOOL_DEFAULT, &vb, NULL));
Cleanup:
    return hr;
}

HRESULT VertexBuffer::SetData(UINT numVertices, void * vertices, DWORD flags)
{
    HRESULT hr = S_OK;
    if (vb)
    {
        char* data;
        IFC(vb->Lock(0, 0, (void**)&data, flags));
        memcpy(data, vertices, numVertices * vertexSize);
        IFC(vb->Unlock());
    }
Cleanup:
    return hr;
}

/// Renders the vertex buffer according to the number of desired primitives
/// device: The device to render on
/// numPrimitives: Total number of desired primitives
/// primitiveType: The type of primitive to render.
void VertexBuffer::Render(LPDIRECT3DDEVICE9 device, UINT numPrimitives, D3DPRIMITIVETYPE primitiveType)
{
    if (device == NULL)
    {
        return;
    }

    device->SetStreamSource(0, vb, 0, vertexSize);
    device->SetFVF(FVF);
    device->DrawPrimitive(primitiveType, 0, numPrimitives);
}
