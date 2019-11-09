#pragma once
#include "Renderer.h"
#include "CubeGeometry.h"
#include "CustomVertex.h"
#include <vector>

class VisualizerRenderer :
    public CRenderer
{
public:
    static HRESULT Create(IDirect3D9 *pD3D, IDirect3D9Ex *pD3DEx, HWND hwnd, UINT uAdapter, CRenderer **ppRenderer);

    HRESULT Render();

    void AdjustRotationSpeed(float newRotationSpeed);

    void AddCube();

    ~VisualizerRenderer();

protected:
    HRESULT Init(IDirect3D9 *pD3D, IDirect3D9Ex *pD3DEx, HWND hwnd, UINT uAdapter);

private:
    VisualizerRenderer();

    float rotationSpeed;

    int numCubes;

    IDirect3DVertexBuffer9 * m_pd3dVB;

    D3DVECTOR currTranslate;

    std::vector<CubeGeometry> geometries;

    void CopyCubes(CUSTOMVERTEX* destination);
};

