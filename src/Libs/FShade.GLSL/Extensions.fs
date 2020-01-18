﻿namespace FShade

open System
open Aardvark.Base
open FShade.Imperative
open FShade.GLSL

[<AutoOpen>]
module Backends =
    let glsl410 =
        Backend.Create {
            version                 = Version(4,1)
            enabledExtensions       = Set.ofList [ ]
            createUniformBuffers    = true
            bindingMode             = BindingMode.PerKind
            createDescriptorSets    = false
            stepDescriptorSets      = false
            createInputLocations    = true
            createPerStageUniforms  = false
            reverseMatrixLogic      = true
        }

    let glsl430 =
        Backend.Create {
            version                 = Version(4,3)
            enabledExtensions       = Set.ofList [ ]
            createUniformBuffers    = true
            bindingMode             = BindingMode.PerKind
            createDescriptorSets    = false
            stepDescriptorSets      = false
            createInputLocations    = true
            createPerStageUniforms  = false
            reverseMatrixLogic      = true
        }

    let glsl120 =
        Backend.Create {
            version                 = Version(1,2)
            enabledExtensions       = Set.empty
            createUniformBuffers    = false
            bindingMode             = BindingMode.None
            createDescriptorSets    = false
            stepDescriptorSets      = false
            createInputLocations    = false
            createPerStageUniforms  = false
            reverseMatrixLogic      = true
        }

    let glslVulkan =
        Backend.Create {
            version                 = Version(4,5)
            enabledExtensions       = Set.ofList [ "GL_ARB_tessellation_shader"; "GL_ARB_separate_shader_objects"; "GL_ARB_shading_language_420pack" ]
            createUniformBuffers    = true
            bindingMode             = BindingMode.Global
            createDescriptorSets    = true
            stepDescriptorSets      = false
            createInputLocations    = true
            createPerStageUniforms  = true
            reverseMatrixLogic      = true
        }

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module ModuleCompiler =

        let private containsCompute (m : Module) =
            m.entries |> List.exists (fun e -> e.decorations |> List.exists (function EntryDecoration.Stages { self = ShaderStage.Compute } -> true | _ -> false))

        let compileGLSL (cfg : Backend) (module_ : Module) =
            let cfg =
                if containsCompute module_ then
                    Backend.Create {
                        cfg.Config with
                            version = Operators.max (Version(4,4,0)) cfg.Config.version
                    }
                else
                    cfg

            module_ 
                |> ModuleCompiler.compile cfg 
                |> Assembler.assemble cfg
                
        let compileGLSL120 (module_ : Module) =
            compileGLSL glsl120 module_

        let compileGLSL410 (module_ : Module) =
            compileGLSL glsl410 module_
            
        let compileGLSL430 (module_ : Module) =
            compileGLSL glsl430 module_

        let compileGLSLVulkan (module_ : Module) =
            compileGLSL glslVulkan module_