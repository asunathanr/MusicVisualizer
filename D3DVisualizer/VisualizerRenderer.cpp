#include "stdafx.h"
#include "VisualizerRenderer.h"
#include <algorithm>

#define D3DFVF_CUSTOMVERTEX (D3DFVF_XYZ | D3DFVF_DIFFUSE)

D3DPRESENT_PARAMETERS d3dpp;

HRESULT VisualizerRenderer::Create(IDirect3D9 * pD3D, IDirect3D9Ex * pD3DEx, HWND hwnd, UINT uAdapter, CRenderer ** ppRenderer)
{
    HRESULT hr = S_OK;

    VisualizerRenderer *pRenderer = new VisualizerRenderer();

    IFCOOM(pRenderer);

    IFC(pRenderer->Init(pD3D, pD3DEx, hwnd, uAdapter));

    *ppRenderer = pRenderer;
    pRenderer = NULL;

    
    ZeroMemory(&d3dpp, sizeof(d3dpp));
    d3dpp.EnableAutoDepthStencil = TRUE;
    d3dpp.AutoDepthStencilFormat = D3DFMT_D16;

Cleanup:
    delete pRenderer;
    return hr;
}

VisualizerRenderer::VisualizerRenderer() : CRenderer(), rotationSpeed(0.0f), m_VB(NULL), currTranslate({0.0f, 0.0f, 0.0f}), numCubes(1), numObjects(0)
{
    
}

VisualizerRenderer::~VisualizerRenderer()
{
    SAFE_RELEASE(m_VB);
}

HRESULT VisualizerRenderer::Init(IDirect3D9 * pD3D, IDirect3D9Ex * pD3DEx, HWND hwnd, UINT uAdapter)
{
        // Set up the VB

    HRESULT hr = S_OK;
    D3DXMATRIXA16 matView, matProj;
    D3DXVECTOR3 vEyePt(0.0f, 0.0f,-5.0f);
    D3DXVECTOR3 vLookatPt(0.0f, 0.0f, 0.0f);
    D3DXVECTOR3 vUpVec(0.0f, 1.0f, 0.0f);


    // Call base to create the device and render target
    IFC(CRenderer::Init(pD3D, pD3DEx, hwnd, uAdapter));

    vb.CreateBuffer(m_pd3dDevice, 1, D3DFVF_CUSTOMVERTEX, sizeof(CUSTOMVERTEX), true);

    // Set up the camera
    D3DXMatrixLookAtLH(&matView, &vEyePt, &vLookatPt, &vUpVec);
    IFC(m_pd3dDevice->SetTransform(D3DTS_VIEW, &matView));
    D3DXMatrixPerspectiveFovLH(&matProj, D3DX_PI / 4, 1.0f, 1.0f, 100.0f);
    IFC(m_pd3dDevice->SetTransform(D3DTS_PROJECTION, &matProj));

    // Set up the global state
    IFC(m_pd3dDevice->SetRenderState(D3DRS_CULLMODE, D3DCULL_NONE));
    IFC(m_pd3dDevice->SetRenderState(D3DRS_LIGHTING, FALSE));
    IFC(m_pd3dDevice->SetStreamSource(0, m_VB, 0, sizeof(CUSTOMVERTEX)));
    IFC(m_pd3dDevice->SetFVF(D3DFVF_CUSTOMVERTEX));

Cleanup:
    return hr;
}

/*
    Renders any vertices currently in the dynamic vertex buffer.
*/
HRESULT VisualizerRenderer::Render()
{
    HRESULT hr = S_OK;
    D3DXMATRIXA16 matWorld;
    D3DXMATRIX* vertexTranslate;

    IFC(m_pd3dDevice->BeginScene());
    IFC(m_pd3dDevice->Clear(
        0,
        NULL,
        D3DCLEAR_TARGET,
        D3DCOLOR_ARGB(128, 28, 28, 28),  // NOTE: Premultiplied alpha!
        1.0f,
        0
        ));
    
    //IFC(m_pd3dDevice->SetTransform(D3DTS_WORLD, &matWorld));
    
    vb.Render(m_pd3dDevice, 1, D3DPT_TRIANGLELIST);

    IFC(m_pd3dDevice->EndScene());

Cleanup:
    return hr;
}

void VisualizerRenderer::AdjustRotationSpeed(float newRotationSpeed)
{
    rotationSpeed = newRotationSpeed;
}

void VisualizerRenderer::AddCube()
{
    ++numCubes;
}

HRESULT VisualizerRenderer::CreateEpoch()
{
    HRESULT hr = S_OK;

    CUSTOMVERTEX vertices[] =
    {
        { -2.0f, -2.0f, 10.0f, 0xffffff00, }, // x, y, z, color
        {  1.0f, -1.0f, 10.0f, 0xff00ff00, },
        {  0.0f,  1.0f, 10.0f, 0xff00ffff, },
    };

    void *pVertices = (void*)vertices;
    IFC(vb.SetData(3, pVertices));
Cleanup:
    return hr;
}

// Private helpers

void VisualizerRenderer::CopyCubes(CUSTOMVERTEX ** destination)
{
    CUSTOMVERTEX ** currentDestination = destination;
    for (CUSTOMVERTEX* vertex : currentVertices) 
    {
        *currentDestination = vertex;
        ++currentDestination;
    }
}

