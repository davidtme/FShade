﻿namespace FShade

open System
open Aardvark.Base


type WrapMode = 
    | Wrap = 0
    | Mirror = 1
    | Clamp = 2
    | Border = 3
    | MirrorOnce = 4
                    
type Filter = 
    | Anisotropic = 0
    | MinLinearMagMipPoint = 1
    | MinLinearMagPointMipLinear = 2
    | MinMagLinearMipPoint = 3
    | MinMagMipLinear = 4
    | MinMagMipPoint = 5
    | MinMagPointMipLinear = 6
    | MinPointMagLinearMipPoint = 7
    | MinPointMagMipLinear = 8
    | MinMagPoint = 9
    | MinMagLinear = 10
    | MinPointMagLinear = 11
    | MinLinearMagPoint = 12
                 
type ComparisonFunction = 
    | Never = 1
    | Less = 2
    | Equal = 3
    | LessOrEqual = 4
    | Greater = 5
    | GreaterOrEqual = 6
    | NotEqual = 7
    | Always = 8
 
type ImageFormat =
    | DepthComponent = 6402
    | R3G3B2 = 10768
    | Rgb4 = 32847
    | Rgb5 = 32848
    | Rgb8 = 32849
    | Rgb10 = 32850
    | Rgb12 = 32851
    | Rgb16 = 32852
    | Rgba2 = 32853
    | Rgba4 = 32854
    | Rgba8 = 32856
    | Rgb10A2 = 32857
    | Rgba12 = 32858
    | Rgba16 = 32859
    | DepthComponent16 = 33189
    | DepthComponent24 = 33190
    | DepthComponent32 = 33191
    | R8 = 33321
    | R16 = 33322
    | Rg8 = 33323
    | Rg16 = 33324
    | R16f = 33325
    | R32f = 33326
    | Rg16f = 33327
    | Rg32f = 33328
    | R8i = 33329
    | R8ui = 33330
    | R16i = 33331
    | R16ui = 33332
    | R32i = 33333
    | R32ui = 33334
    | Rg8i = 33335
    | Rg8ui = 33336
    | Rg16i = 33337
    | Rg16ui = 33338
    | Rg32i = 33339
    | Rg32ui = 33340
    | DepthStencil = 34041
    | Rgba32f = 34836
    | Rgb32f = 34837
    | Rgba16f = 34842
    | Rgb16f = 34843
    | Depth24Stencil8 = 35056
    | R11fG11fB10f = 35898
    | Rgb9E5 = 35901
    | Srgb8 = 35905
    | Srgb8Alpha8 = 35907
    | DepthComponent32f = 36012
    | Depth32fStencil8 = 36013
    | StencilIndex1Ext = 36166
    | StencilIndex1 = 36166
    | StencilIndex4Ext = 36167
    | StencilIndex4 = 36167
    | StencilIndex8 = 36168
    | StencilIndex8Ext = 36168
    | StencilIndex16Ext = 36169
    | StencilIndex16 = 36169
    | Rgba32ui = 36208
    | Rgb32ui = 36209
    | Rgba16ui = 36214
    | Rgb16ui = 36215
    | Rgba8ui = 36220
    | Rgb8ui = 36221
    | Rgba32i = 36226
    | Rgb32i = 36227
    | Rgba16i = 36232
    | Rgb16i = 36233
    | Rgba8i = 36238
    | Rgb8i = 36239
    | Rgb10A2ui = 36975

[<NoComparison>]      
type SamplerState = 
    {
        AddressU : Option<WrapMode>
        AddressV : Option<WrapMode>
        AddressW : Option<WrapMode>
        Filter : Option<Filter>
        Comparison : Option<ComparisonFunction>
        BorderColor : Option<C4f>
        MaxAnisotropy : Option<int>
        MaxLod : Option<float>
        MinLod : Option<float>
        MipLodBias : Option<float>
    }
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module SamplerState =
    let empty = 
        { 
            AddressU = None
            AddressV = None
            AddressW = None
            Filter = None
            Comparison = None
            BorderColor = None
            MaxAnisotropy = None
            MaxLod = None
            MinLod = None
            MipLodBias = None 
        }


type SamplerType = 
    | Float = 0
    | Int = 1

type SamplerDimension = 
    | Sampler1d = 0
    | Sampler2d = 1
    | Sampler3d = 2
    | SamplerCube = 3


[<AutoOpen>]
module SamplerExtensions =
    type SamplerStateBuilder() =
        member x.Yield(_) = SamplerState.empty

        [<CustomOperation("addressU")>]
        member x.AddressU(h : SamplerState, w : WrapMode) = { h with AddressU = Some w }
             
        [<CustomOperation("addressV")>]
        member x.AddressV(h : SamplerState, w : WrapMode) = { h with AddressV = Some w }
             
        [<CustomOperation("addressW")>]
        member x.AddressW(h : SamplerState, w : WrapMode) = { h with AddressW = Some w }
             
        [<CustomOperation("maxAnisotropy")>]
        member x.MaxAnisotropy(h : SamplerState, a : int) = { h with MaxAnisotropy = Some a }
             
        [<CustomOperation("borderColor")>]
        member x.BorderColor(h : SamplerState, c : C4f) = { h with BorderColor = Some c }
             
        [<CustomOperation("maxLod")>]
        member x.MaxLod(h : SamplerState, c : float) = { h with MaxLod = Some c }
             
        [<CustomOperation("minLod")>]
        member x.MinLod(h : SamplerState, c : float) = { h with MinLod = Some c }
             
        [<CustomOperation("mipLodBias")>]
        member x.MipLodBias(h : SamplerState, c : float) = { h with MipLodBias = Some c }
             
        [<CustomOperation("filter")>]
        member x.Filter(h : SamplerState, f : Filter) = { h with Filter = Some f }
      
    let samplerState = SamplerStateBuilder()

