#pragma once
#include "Renderer.h"
class VisualizerRenderer :
    public CRenderer
{
public:
    static HRESULT Create(IDirect3D9 *pD3D, IDirect3D9Ex *pD3DEx, HWND hwnd, UINT uAdapter, CRenderer **ppRenderer);

    HRESULT Render();

    void AdjustRotationSpeed(float newRotationSpeed);

    ~VisualizerRenderer();

protected:
    HRESULT Init(IDirect3D9 *pD3D, IDirect3D9Ex *pD3DEx, HWND hwnd, UINT uAdapter);

private:
    VisualizerRenderer();

    float rotationSpeed;

    IDirect3DVertexBuffer9 *m_pd3dVB;
};
