#pragma once

#include <d3d9.h>
#include <directxcolors.h> 

// From: https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/walkthrough-creating-direct3d9-content-for-hosting-in-wpf?view=netframework-4.8

class CRenderer;

class CRendererManager
{
public:
    static HRESULT Create(CRendererManager **ppManager);
    ~CRendererManager();

    HRESULT EnsureDevices();

    void AdjustRotationSpeed(float newSpeed);
    void CreateEpoch();
    void SetSize(UINT uWidth, UINT uHeight);
    void SetAlpha(bool fUseAlpha);
    void SetNumDesiredSamples(UINT uNumSamples);
    void SetAdapter(POINT screenSpacePoint);

    HRESULT GetBackBufferNoRef(IDirect3DSurface9 **ppSurface);

    HRESULT Render();

    
private:
    CRendererManager();

    void CleanupInvalidDevices();
    HRESULT EnsureRenderers();
    HRESULT EnsureHWND();
    HRESULT EnsureD3DObjects();
    HRESULT TestSurfaceSettings();
    void DestroyResources();

    IDirect3D9    *m_pD3D;
    IDirect3D9Ex  *m_pD3DEx;

    UINT m_cAdapters;
    CRenderer **m_rgRenderers;
    CRenderer *m_pCurrentRenderer;

    HWND m_hwnd;

    UINT m_uWidth;
    UINT m_uHeight;
    UINT m_uNumSamples;
    bool m_fUseAlpha;
    bool m_fSurfaceSettingsChanged;
};

