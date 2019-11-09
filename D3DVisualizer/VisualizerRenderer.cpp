#include "stdafx.h"
#include "VisualizerRenderer.h"
#include <algorithm>

#define D3DFVF_CUSTOMVERTEX (D3DFVF_XYZ | D3DFVF_DIFFUSE)

HRESULT VisualizerRenderer::Create(IDirect3D9 * pD3D, IDirect3D9Ex * pD3DEx, HWND hwnd, UINT uAdapter, CRenderer ** ppRenderer)
{
    HRESULT hr = S_OK;

    VisualizerRenderer *pRenderer = new VisualizerRenderer();

    IFCOOM(pRenderer);

    IFC(pRenderer->Init(pD3D, pD3DEx, hwnd, uAdapter));

    *ppRenderer = pRenderer;
    pRenderer = NULL;

Cleanup:
    delete pRenderer;
    return hr;
}

VisualizerRenderer::VisualizerRenderer() : CRenderer(), rotationSpeed(0.0f), m_pd3dVB(NULL), currTranslate({0.0f, 0.0f, 0.0f}), numCubes(1)
{
}



VisualizerRenderer::~VisualizerRenderer()
{
    SAFE_RELEASE(m_pd3dVB);
}

HRESULT VisualizerRenderer::Init(IDirect3D9 * pD3D, IDirect3D9Ex * pD3DEx, HWND hwnd, UINT uAdapter)
{
        // Set up the VB
    /*
    CUSTOMVERTEX vertices[] =
    {
        { 1.0f, 1.0f, 0.0f, 0xffffff00, }, // x, y, z, color
        {  1.0f, 2.0f, 0.0f, 0xff00ff00, },
        {  2.0f,  2.0f, 0.0f, 0xff00ffff, },

        { 1.0f, 1.0f, 0.0f, 0xffffff00, }, // x, y, z, color
        {  2.0f, 1.0f, 0.0f, 0xff00ff00, }, // Bottom-right
        {  2.0f,  2.0f, 0.0f, 0xff00ffff, },
    };
    */

    CUSTOMVERTEX vertices[36];
    geometries.push_back(CubeGeometry({-1.0f, -1.0f, 0.0f}, 1.0f));
    CopyCubes(vertices);

    HRESULT hr = S_OK;
    D3DXMATRIXA16 matView, matProj;
    D3DXVECTOR3 vEyePt(0.0f, 0.0f,-5.0f);
    D3DXVECTOR3 vLookatPt(0.0f, 0.0f, 0.0f);
    D3DXVECTOR3 vUpVec(0.0f, 1.0f, 0.0f);


    UINT numVertices = sizeof(CUSTOMVERTEX) * 36;

    // Call base to create the device and render target
    IFC(CRenderer::Init(pD3D, pD3DEx, hwnd, uAdapter));

    IFC(m_pd3dDevice->CreateVertexBuffer(sizeof(vertices), 0, D3DFVF_CUSTOMVERTEX, D3DPOOL_DEFAULT, &m_pd3dVB, NULL));

    void *pVertices;
    IFC(m_pd3dVB->Lock(0, sizeof(vertices), &pVertices, 0));
    memcpy(pVertices, vertices, sizeof(vertices));
    m_pd3dVB->Unlock();

    // Set up the camera
    D3DXMatrixLookAtLH(&matView, &vEyePt, &vLookatPt, &vUpVec);
    IFC(m_pd3dDevice->SetTransform(D3DTS_VIEW, &matView));
    D3DXMatrixPerspectiveFovLH(&matProj, D3DX_PI / 4, 1.0f, 1.0f, 100.0f);
    IFC(m_pd3dDevice->SetTransform(D3DTS_PROJECTION, &matProj));

    // Set up the global state
    IFC(m_pd3dDevice->SetRenderState(D3DRS_CULLMODE, D3DCULL_NONE));
    IFC(m_pd3dDevice->SetRenderState(D3DRS_LIGHTING, FALSE));
    IFC(m_pd3dDevice->SetStreamSource(0, m_pd3dVB, 0, sizeof(CUSTOMVERTEX)));
    IFC(m_pd3dDevice->SetFVF(D3DFVF_CUSTOMVERTEX));

Cleanup:
    return hr;
}


HRESULT VisualizerRenderer::Render()
{
    HRESULT hr = S_OK;
    D3DXMATRIXA16 matWorld;
    // Set up the rotation
    UINT  iTime  = GetTickCount() % 1000;
    FLOAT fAngle = rotationSpeed * iTime * (2.0f * D3DX_PI) / 1000.0f;

    IFC(m_pd3dDevice->BeginScene());
    IFC(m_pd3dDevice->Clear(
        0,
        NULL,
        D3DCLEAR_TARGET,
        D3DCOLOR_ARGB(128, 0, 0, 128),  // NOTE: Premultiplied alpha!
        1.0f,
        0
        ));
    
    D3DXMatrixRotationY(&matWorld, fAngle);
    //D3DXMatrixTranslation(&matWorld, currTranslate.x, currTranslate.y, currTranslate.z);
    
    IFC(m_pd3dDevice->SetTransform(D3DTS_WORLD, &matWorld));

    IFC(m_pd3dDevice->DrawPrimitive(D3DPT_TRIANGLELIST, 0, 12));

    IFC(m_pd3dDevice->EndScene());
    currTranslate.z -= 0.00f;

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

void VisualizerRenderer::CopyCubes(CUSTOMVERTEX * destination)
{
    CUSTOMVERTEX * currentDestination = destination;
    for (const CubeGeometry& geometry : geometries) 
    {
        for (int i = 0; i < 36; ++i) 
        {
            *currentDestination = geometry.vertices[i];
            currentDestination++;
        }
    }
}

