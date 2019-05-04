#include "AppWindow.h"
#include <Windows.h>

struct vec3 {
	float x, y, z;
};

struct vertex {
	vec3 position;
	vec3 position1;
	vec3 color;
	vec3 color1;
};

__declspec(align(16))
struct constant {
	unsigned int time;
};


AppWindow::AppWindow() {}


AppWindow::~AppWindow() {}

void AppWindow::OnCreate() {
	Window::OnCreate();
	GraphicsEngine::Get()->Init();
	_swapChain = GraphicsEngine::Get()->CreateSwapChain();

	RECT rc = this->GetClientWindowRect();
	_swapChain->Init(this->_hwnd, rc.right - rc.left, rc.bottom - rc.top);

	//vertex list[] = {
	//	{-0.5f,-0.5f,0.0f}, // POS1
	//	{-0.5f, 0.5f,0.0f}, // POS2
	//	{ 0.5f,0.5f,0.0f},

	//	{ 0.5f,0.5f,0.0f }, // POS1
	//	{0.5f, -0.5f,0.0f}, // POS2
	//	{-0.5f,-0.5f,0.0f}
	//};

	vertex list[] = {
		{-0.5f,-0.5f,0.0f,   -0.32f, -0.11f, 0.0f,		0, 0, 0,	0, 1, 0}, // POS1
		{-0.5f, 0.5f,0.0f,   -0.11f, 0.78f, 0.0f,       1, 1, 0,	0, 1, 1}, // POS2
		{ 0.5f,-0.5f,0.0f,   0.75f, -0.73f, 0.0f,	    0, 0, 1,	1, 0, 0},
		{ 0.5f,0.5f,0.0f,    0.88f, 0.77f, 0.0f,		1, 1, 1,	0, 0, 1} // POS11
	};

	_vertexBuffer = GraphicsEngine::Get()->CreateVertexBuffer();
	UINT sizeList = ARRAYSIZE(list);

	void* shaderByteCode = nullptr;
	size_t shaderSize = 0;
	GraphicsEngine::Get()->CompileVertexShader(L"VertexShader.hlsl", "vsmain", &shaderByteCode, &shaderSize);

	_vertexShader = GraphicsEngine::Get()->CreateVertexShader(shaderByteCode, shaderSize);

	bool res = _vertexBuffer->Load(list, sizeof(vertex), sizeList, shaderByteCode, shaderSize);

	GraphicsEngine::Get()->ReleaseCompiledShader();



	GraphicsEngine::Get()->CompilePixelShader(L"PixelShader.hlsl", "psmain", &shaderByteCode, &shaderSize);

	_pixelShader = GraphicsEngine::Get()->CreatePixelShader(shaderByteCode, shaderSize);

	GraphicsEngine::Get()->ReleaseCompiledShader();


	constant cc;

	cc.time = 0;

	_constantBuffer = GraphicsEngine::Get()->CreateConstantBuffer();

	_constantBuffer->Load(&cc, sizeof(constant));
}

void AppWindow::OnUpdate() {
	Window::OnUpdate();
	GraphicsEngine::Get()->GetImmediateDeviceContext()->ClearRenderTargetColor(this->_swapChain, 0, 0, 1, 1);

	RECT rc = this->GetClientWindowRect();
	GraphicsEngine::Get()->GetImmediateDeviceContext()->SetViewportSize(rc.right - rc.left, rc.bottom - rc.top);

	constant cc;
	cc.time = ::GetTickCount();

	_constantBuffer->Update(GraphicsEngine::Get()->GetImmediateDeviceContext(), &cc);

	GraphicsEngine::Get()->GetImmediateDeviceContext()->SetConstantBuffer(_vertexShader, _constantBuffer);
	GraphicsEngine::Get()->GetImmediateDeviceContext()->SetConstantBuffer(_pixelShader, _constantBuffer);

	GraphicsEngine::Get()->GetImmediateDeviceContext()->SetVertexShader(_vertexShader);
	GraphicsEngine::Get()->GetImmediateDeviceContext()->SetPixelShader(_pixelShader);

	GraphicsEngine::Get()->GetImmediateDeviceContext()->SetVertexBuffer(_vertexBuffer);

	//GraphicsEngine::Get()->GetImmediateDeviceContext()->DrawTriangleList(_vertexBuffer->GetSizeVertexList(), 0);
	GraphicsEngine::Get()->GetImmediateDeviceContext()->DrawTriangleStrip(_vertexBuffer->GetSizeVertexList(), 0);

	_swapChain->Present(false);
}

void AppWindow::OnDestroy() {
	Window::OnDestroy();

	_swapChain->Release();
	_vertexBuffer->Release();
	_vertexShader->Release();
	_pixelShader->Release();
	GraphicsEngine::Get()->Release();
}
