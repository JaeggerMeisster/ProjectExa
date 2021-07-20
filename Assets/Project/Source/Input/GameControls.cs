// GENERATED AUTOMATICALLY FROM 'Assets/Project/Source/Input/GameControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Exa.Input
{
    public class @GameControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @GameControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""Editor"",
            ""id"": ""ddda1cd8-bd9b-4895-b667-214d683e0e16"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""d4726475-8b79-45d1-9532-28ed98f0eec0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""6ce42356-795d-497f-a813-8f86d4d0bca7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""bb03ea49-7fd7-43e5-bf85-acd7f986412d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateLeft"",
                    ""type"": ""Button"",
                    ""id"": ""74076920-1a4a-44c6-9eed-0a5e545732ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateRight"",
                    ""type"": ""Button"",
                    ""id"": ""0c25f458-dd75-4756-9108-297733f06b3d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleVerticalMirror"",
                    ""type"": ""Button"",
                    ""id"": ""5cd15413-dd5d-4c6c-aed7-035969a257fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""491d8e4b-11b2-40f6-9188-67d4ab7e0bf7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f000d51a-3bf6-494e-b022-1e27fdf2901a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a9978e7d-5891-4e26-9cea-3d38037b4720"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c6d95be6-8d84-46cd-9358-85985e7f8fd7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""193be172-a61e-489b-82ec-9428a2c1da95"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8aa6fcb9-dbde-42be-a650-cec1a9150dd7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""99cab014-b995-403d-85ad-3140c01037aa"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16aee5bf-7843-4f6b-ad08-eac99f6a0cb5"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16af04c7-5a85-4ef6-8f5d-14083def649a"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""RotateLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e8e8c7ea-a53f-4182-b207-e1ba6b69c473"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""RotateRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9edb89d0-2990-477b-8851-8db2e565f78b"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""ToggleVerticalMirror"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d592b28c-df45-4c5d-a5bb-a3577412b6e0"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Debug"",
            ""id"": ""f0082add-904f-492a-abda-60fefe8da8ff"",
            ""actions"": [
                {
                    ""name"": ""ToggleConsole"",
                    ""type"": ""Button"",
                    ""id"": ""7e486db0-7abd-4570-86e6-53b181833168"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drag"",
                    ""type"": ""Button"",
                    ""id"": ""0b87a355-27b6-40d2-842e-51238e7d1ac3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""84af3c11-3317-4802-9f51-fb60c2435bab"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""33ac8a8d-0546-4121-8858-7134b251890b"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""ToggleConsole"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02fde2ea-86ba-48b5-9360-8600e5ef0a0e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""51bb6693-5d5d-45f2-851d-ab9734690b0d"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=-1)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5587d601-81ce-48bd-adc0-afd4297eef95"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ReturnNavigateable"",
            ""id"": ""c8b9e2d9-a691-4930-80bc-7f36809a9bb6"",
            ""actions"": [
                {
                    ""name"": ""Return"",
                    ""type"": ""Button"",
                    ""id"": ""9f2ee82f-6b2c-4de1-834d-8b988278babe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8c5a4399-91ac-4590-8129-3ded1ee8fdc0"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Return"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Gameplay"",
            ""id"": ""61cd0ddd-a0ce-4af1-8555-837eaeb9cd6a"",
            ""actions"": [
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c7195519-c99e-4f5a-af47-8d0d9164bc25"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""50bb47ae-7f50-45b0-9e96-b252abb46662"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""9ee6f204-606b-4201-b062-76a13c822018"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""b82d7752-30f4-4315-95fd-e85d984f38bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""124fa870-a9a7-4c9a-a2f6-5462f85096a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SaveGroup"",
                    ""type"": ""Value"",
                    ""id"": ""deada82f-f5b3-47ed-95c4-63ebf35b4af5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectGroup"",
                    ""type"": ""Value"",
                    ""id"": ""f1457bf0-3320-4d7d-86b7-f7ddc25cd9e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SaveGroupModifier"",
                    ""type"": ""Button"",
                    ""id"": ""ecbfdfaf-389d-481b-b072-9bd6a0a1d0e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2aac21f5-8e79-40b5-bedc-1f0ed276a573"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6a5c9fe1-92cc-4dfe-8bbf-8154a0976c52"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""958d0fe2-0af9-4fe2-8256-d75bff1922a8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""80b671d7-af07-4bb8-92ec-646be3a33dbd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""65e4f846-71df-467c-87f8-fdb53031968f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0e38678e-a39c-47fc-9884-a7664d3f7b1b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""483a399c-6bc9-4b68-9014-8661d8c4175b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5df59e7-36ee-46a3-9c25-70f0d52a7153"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05f9f505-f901-4b0a-a703-43e6e97b6bcb"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""575af4d0-e09b-4525-aee3-a94e51b2bbff"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SelectGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""604737ac-8321-462d-b3aa-8e72f348fbdc"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=2)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SelectGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c74fed5-23ba-4264-9f93-91e461a1ad8a"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=3)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SelectGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58683b3d-93f4-4246-9eb2-e295fd21a6a3"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=4)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SelectGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d4a7c05-d969-4aec-8a36-bffe7da6e4e7"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=5)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SelectGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ea73dbb-4b0a-438b-9080-3d0fdaf70939"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=6)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SelectGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2efd8d0-0116-4309-9404-dbf51f179155"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=7)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SelectGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec1355f6-abd8-4305-8180-4cd1ff49fa90"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=8)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SelectGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""885ff937-4a91-45cb-9f6d-c5651d283870"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=9)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SelectGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8debbf68-4854-4872-9467-6bd60d8596f5"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=0)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SelectGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1"",
                    ""id"": ""2b7dfe7c-c23d-49d6-9f64-c5079c4e9e68"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""2637ba8a-e4b2-4c9d-b2ae-11a6f4c72255"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""156d8dcd-b0cf-459a-9136-b60794a3a273"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2"",
                    ""id"": ""279c576c-f5dc-499e-8f66-c87cc4ce18a8"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=2)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""e401e56e-2bda-4c4a-924b-61c92884b822"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""9d8be808-9227-4625-8970-a218f02fb607"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""3"",
                    ""id"": ""bb344562-e57a-4506-b1da-bff57e049fc0"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=3)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""5ef450c6-67a1-4c2e-b9b3-393b70ef5bf9"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""f4b5b814-0aa4-4da7-a0db-b9d2db3ea9e0"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""4"",
                    ""id"": ""41b0d1b2-1794-43ce-8c93-d2e96fcc1a3c"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=4)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""3a3037b5-c81d-4430-a46f-9ca729e5b76e"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""6847251d-5e4f-4bb5-8843-b2ee9c09c539"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""5"",
                    ""id"": ""2562895f-2dcd-4d9d-bafa-974dc6c845cc"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=5)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""7fea6236-cdf3-4267-9f60-7102fcde6187"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""8e2da8db-4c81-4fc6-b29b-21ad5be7543f"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""6"",
                    ""id"": ""befc81ec-8f85-4633-a82e-d49bf8ab48a4"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=6)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""4f4fc270-50eb-48b3-828d-6c8c55867fb6"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""af3d9435-48c1-4336-9bf8-c54945e73ca1"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""7"",
                    ""id"": ""998ab962-29e6-4d08-84d4-ef9f583c38dc"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=7)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""b369c8a8-ca96-4ad2-9014-11ce3f95c72f"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""dab5903f-30ef-4df4-87e9-3fdf3d1a62be"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""8"",
                    ""id"": ""77f5c2bb-f652-4df9-ab4a-703d7e27ff78"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=8)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""c0d21057-5c4c-4d00-8c44-5307a1982339"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""40983828-8fda-4685-9b93-a6a60899cbd0"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""9"",
                    ""id"": ""e78982ac-fbd0-44fe-ab98-45b21388f857"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=9)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""c260d75c-1de6-404d-9ca2-5354c5a204ff"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""055c8606-3d9d-4fe2-b79f-544dc8dd59bb"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""0"",
                    ""id"": ""eb22cc86-bce2-4160-ad17-c1b7691df9e0"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": ""Scale(factor=0)"",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""8506a5fc-2282-46aa-ada1-0278cb1c0259"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""1bf6f92f-8ee5-4920-8a05-519442b62854"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2295beaf-044b-4589-9f30-56a42d85decd"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""SaveGroupModifier"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerStation"",
            ""id"": ""d0b94873-f500-4793-a15b-0dac208926b7"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""5d02f292-a019-4882-b2d9-e5b82ca30e1b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ade5d63f-cadd-425b-839d-78b4aa859b3a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKb"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""MouseKb"",
            ""bindingGroup"": ""MouseKb"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Editor
            m_Editor = asset.FindActionMap("Editor", throwIfNotFound: true);
            m_Editor_Movement = m_Editor.FindAction("Movement", throwIfNotFound: true);
            m_Editor_LeftClick = m_Editor.FindAction("LeftClick", throwIfNotFound: true);
            m_Editor_RightClick = m_Editor.FindAction("RightClick", throwIfNotFound: true);
            m_Editor_RotateLeft = m_Editor.FindAction("RotateLeft", throwIfNotFound: true);
            m_Editor_RotateRight = m_Editor.FindAction("RotateRight", throwIfNotFound: true);
            m_Editor_ToggleVerticalMirror = m_Editor.FindAction("ToggleVerticalMirror", throwIfNotFound: true);
            m_Editor_Zoom = m_Editor.FindAction("Zoom", throwIfNotFound: true);
            // Debug
            m_Debug = asset.FindActionMap("Debug", throwIfNotFound: true);
            m_Debug_ToggleConsole = m_Debug.FindAction("ToggleConsole", throwIfNotFound: true);
            m_Debug_Drag = m_Debug.FindAction("Drag", throwIfNotFound: true);
            m_Debug_Rotate = m_Debug.FindAction("Rotate", throwIfNotFound: true);
            // ReturnNavigateable
            m_ReturnNavigateable = asset.FindActionMap("ReturnNavigateable", throwIfNotFound: true);
            m_ReturnNavigateable_Return = m_ReturnNavigateable.FindAction("Return", throwIfNotFound: true);
            // Gameplay
            m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
            m_Gameplay_Zoom = m_Gameplay.FindAction("Zoom", throwIfNotFound: true);
            m_Gameplay_RightClick = m_Gameplay.FindAction("RightClick", throwIfNotFound: true);
            m_Gameplay_LeftClick = m_Gameplay.FindAction("LeftClick", throwIfNotFound: true);
            m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
            m_Gameplay_Escape = m_Gameplay.FindAction("Escape", throwIfNotFound: true);
            m_Gameplay_SaveGroup = m_Gameplay.FindAction("SaveGroup", throwIfNotFound: true);
            m_Gameplay_SelectGroup = m_Gameplay.FindAction("SelectGroup", throwIfNotFound: true);
            m_Gameplay_SaveGroupModifier = m_Gameplay.FindAction("SaveGroupModifier", throwIfNotFound: true);
            // PlayerStation
            m_PlayerStation = asset.FindActionMap("PlayerStation", throwIfNotFound: true);
            m_PlayerStation_Fire = m_PlayerStation.FindAction("Fire", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Editor
        private readonly InputActionMap m_Editor;
        private IEditorActions m_EditorActionsCallbackInterface;
        private readonly InputAction m_Editor_Movement;
        private readonly InputAction m_Editor_LeftClick;
        private readonly InputAction m_Editor_RightClick;
        private readonly InputAction m_Editor_RotateLeft;
        private readonly InputAction m_Editor_RotateRight;
        private readonly InputAction m_Editor_ToggleVerticalMirror;
        private readonly InputAction m_Editor_Zoom;
        public struct EditorActions
        {
            private @GameControls m_Wrapper;
            public EditorActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_Editor_Movement;
            public InputAction @LeftClick => m_Wrapper.m_Editor_LeftClick;
            public InputAction @RightClick => m_Wrapper.m_Editor_RightClick;
            public InputAction @RotateLeft => m_Wrapper.m_Editor_RotateLeft;
            public InputAction @RotateRight => m_Wrapper.m_Editor_RotateRight;
            public InputAction @ToggleVerticalMirror => m_Wrapper.m_Editor_ToggleVerticalMirror;
            public InputAction @Zoom => m_Wrapper.m_Editor_Zoom;
            public InputActionMap Get() { return m_Wrapper.m_Editor; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(EditorActions set) { return set.Get(); }
            public void SetCallbacks(IEditorActions instance)
            {
                if (m_Wrapper.m_EditorActionsCallbackInterface != null)
                {
                    @Movement.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnMovement;
                    @LeftClick.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnLeftClick;
                    @LeftClick.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnLeftClick;
                    @LeftClick.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnLeftClick;
                    @RightClick.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnRightClick;
                    @RightClick.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnRightClick;
                    @RightClick.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnRightClick;
                    @RotateLeft.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnRotateLeft;
                    @RotateLeft.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnRotateLeft;
                    @RotateLeft.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnRotateLeft;
                    @RotateRight.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnRotateRight;
                    @RotateRight.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnRotateRight;
                    @RotateRight.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnRotateRight;
                    @ToggleVerticalMirror.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnToggleVerticalMirror;
                    @ToggleVerticalMirror.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnToggleVerticalMirror;
                    @ToggleVerticalMirror.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnToggleVerticalMirror;
                    @Zoom.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnZoom;
                    @Zoom.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnZoom;
                    @Zoom.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnZoom;
                }
                m_Wrapper.m_EditorActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @LeftClick.started += instance.OnLeftClick;
                    @LeftClick.performed += instance.OnLeftClick;
                    @LeftClick.canceled += instance.OnLeftClick;
                    @RightClick.started += instance.OnRightClick;
                    @RightClick.performed += instance.OnRightClick;
                    @RightClick.canceled += instance.OnRightClick;
                    @RotateLeft.started += instance.OnRotateLeft;
                    @RotateLeft.performed += instance.OnRotateLeft;
                    @RotateLeft.canceled += instance.OnRotateLeft;
                    @RotateRight.started += instance.OnRotateRight;
                    @RotateRight.performed += instance.OnRotateRight;
                    @RotateRight.canceled += instance.OnRotateRight;
                    @ToggleVerticalMirror.started += instance.OnToggleVerticalMirror;
                    @ToggleVerticalMirror.performed += instance.OnToggleVerticalMirror;
                    @ToggleVerticalMirror.canceled += instance.OnToggleVerticalMirror;
                    @Zoom.started += instance.OnZoom;
                    @Zoom.performed += instance.OnZoom;
                    @Zoom.canceled += instance.OnZoom;
                }
            }
        }
        public EditorActions @Editor => new EditorActions(this);

        // Debug
        private readonly InputActionMap m_Debug;
        private IDebugActions m_DebugActionsCallbackInterface;
        private readonly InputAction m_Debug_ToggleConsole;
        private readonly InputAction m_Debug_Drag;
        private readonly InputAction m_Debug_Rotate;
        public struct DebugActions
        {
            private @GameControls m_Wrapper;
            public DebugActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @ToggleConsole => m_Wrapper.m_Debug_ToggleConsole;
            public InputAction @Drag => m_Wrapper.m_Debug_Drag;
            public InputAction @Rotate => m_Wrapper.m_Debug_Rotate;
            public InputActionMap Get() { return m_Wrapper.m_Debug; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(DebugActions set) { return set.Get(); }
            public void SetCallbacks(IDebugActions instance)
            {
                if (m_Wrapper.m_DebugActionsCallbackInterface != null)
                {
                    @ToggleConsole.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnToggleConsole;
                    @ToggleConsole.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnToggleConsole;
                    @ToggleConsole.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnToggleConsole;
                    @Drag.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnDrag;
                    @Drag.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnDrag;
                    @Drag.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnDrag;
                    @Rotate.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnRotate;
                    @Rotate.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnRotate;
                    @Rotate.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnRotate;
                }
                m_Wrapper.m_DebugActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @ToggleConsole.started += instance.OnToggleConsole;
                    @ToggleConsole.performed += instance.OnToggleConsole;
                    @ToggleConsole.canceled += instance.OnToggleConsole;
                    @Drag.started += instance.OnDrag;
                    @Drag.performed += instance.OnDrag;
                    @Drag.canceled += instance.OnDrag;
                    @Rotate.started += instance.OnRotate;
                    @Rotate.performed += instance.OnRotate;
                    @Rotate.canceled += instance.OnRotate;
                }
            }
        }
        public DebugActions @Debug => new DebugActions(this);

        // ReturnNavigateable
        private readonly InputActionMap m_ReturnNavigateable;
        private IReturnNavigateableActions m_ReturnNavigateableActionsCallbackInterface;
        private readonly InputAction m_ReturnNavigateable_Return;
        public struct ReturnNavigateableActions
        {
            private @GameControls m_Wrapper;
            public ReturnNavigateableActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Return => m_Wrapper.m_ReturnNavigateable_Return;
            public InputActionMap Get() { return m_Wrapper.m_ReturnNavigateable; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ReturnNavigateableActions set) { return set.Get(); }
            public void SetCallbacks(IReturnNavigateableActions instance)
            {
                if (m_Wrapper.m_ReturnNavigateableActionsCallbackInterface != null)
                {
                    @Return.started -= m_Wrapper.m_ReturnNavigateableActionsCallbackInterface.OnReturn;
                    @Return.performed -= m_Wrapper.m_ReturnNavigateableActionsCallbackInterface.OnReturn;
                    @Return.canceled -= m_Wrapper.m_ReturnNavigateableActionsCallbackInterface.OnReturn;
                }
                m_Wrapper.m_ReturnNavigateableActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Return.started += instance.OnReturn;
                    @Return.performed += instance.OnReturn;
                    @Return.canceled += instance.OnReturn;
                }
            }
        }
        public ReturnNavigateableActions @ReturnNavigateable => new ReturnNavigateableActions(this);

        // Gameplay
        private readonly InputActionMap m_Gameplay;
        private IGameplayActions m_GameplayActionsCallbackInterface;
        private readonly InputAction m_Gameplay_Zoom;
        private readonly InputAction m_Gameplay_RightClick;
        private readonly InputAction m_Gameplay_LeftClick;
        private readonly InputAction m_Gameplay_Movement;
        private readonly InputAction m_Gameplay_Escape;
        private readonly InputAction m_Gameplay_SaveGroup;
        private readonly InputAction m_Gameplay_SelectGroup;
        private readonly InputAction m_Gameplay_SaveGroupModifier;
        public struct GameplayActions
        {
            private @GameControls m_Wrapper;
            public GameplayActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Zoom => m_Wrapper.m_Gameplay_Zoom;
            public InputAction @RightClick => m_Wrapper.m_Gameplay_RightClick;
            public InputAction @LeftClick => m_Wrapper.m_Gameplay_LeftClick;
            public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
            public InputAction @Escape => m_Wrapper.m_Gameplay_Escape;
            public InputAction @SaveGroup => m_Wrapper.m_Gameplay_SaveGroup;
            public InputAction @SelectGroup => m_Wrapper.m_Gameplay_SelectGroup;
            public InputAction @SaveGroupModifier => m_Wrapper.m_Gameplay_SaveGroupModifier;
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void SetCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
                {
                    @Zoom.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
                    @Zoom.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
                    @Zoom.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnZoom;
                    @RightClick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightClick;
                    @RightClick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightClick;
                    @RightClick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightClick;
                    @LeftClick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftClick;
                    @LeftClick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftClick;
                    @LeftClick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftClick;
                    @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Escape.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEscape;
                    @Escape.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEscape;
                    @Escape.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEscape;
                    @SaveGroup.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSaveGroup;
                    @SaveGroup.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSaveGroup;
                    @SaveGroup.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSaveGroup;
                    @SelectGroup.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectGroup;
                    @SelectGroup.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectGroup;
                    @SelectGroup.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectGroup;
                    @SaveGroupModifier.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSaveGroupModifier;
                    @SaveGroupModifier.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSaveGroupModifier;
                    @SaveGroupModifier.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSaveGroupModifier;
                }
                m_Wrapper.m_GameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Zoom.started += instance.OnZoom;
                    @Zoom.performed += instance.OnZoom;
                    @Zoom.canceled += instance.OnZoom;
                    @RightClick.started += instance.OnRightClick;
                    @RightClick.performed += instance.OnRightClick;
                    @RightClick.canceled += instance.OnRightClick;
                    @LeftClick.started += instance.OnLeftClick;
                    @LeftClick.performed += instance.OnLeftClick;
                    @LeftClick.canceled += instance.OnLeftClick;
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @Escape.started += instance.OnEscape;
                    @Escape.performed += instance.OnEscape;
                    @Escape.canceled += instance.OnEscape;
                    @SaveGroup.started += instance.OnSaveGroup;
                    @SaveGroup.performed += instance.OnSaveGroup;
                    @SaveGroup.canceled += instance.OnSaveGroup;
                    @SelectGroup.started += instance.OnSelectGroup;
                    @SelectGroup.performed += instance.OnSelectGroup;
                    @SelectGroup.canceled += instance.OnSelectGroup;
                    @SaveGroupModifier.started += instance.OnSaveGroupModifier;
                    @SaveGroupModifier.performed += instance.OnSaveGroupModifier;
                    @SaveGroupModifier.canceled += instance.OnSaveGroupModifier;
                }
            }
        }
        public GameplayActions @Gameplay => new GameplayActions(this);

        // PlayerStation
        private readonly InputActionMap m_PlayerStation;
        private IPlayerStationActions m_PlayerStationActionsCallbackInterface;
        private readonly InputAction m_PlayerStation_Fire;
        public struct PlayerStationActions
        {
            private @GameControls m_Wrapper;
            public PlayerStationActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Fire => m_Wrapper.m_PlayerStation_Fire;
            public InputActionMap Get() { return m_Wrapper.m_PlayerStation; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerStationActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerStationActions instance)
            {
                if (m_Wrapper.m_PlayerStationActionsCallbackInterface != null)
                {
                    @Fire.started -= m_Wrapper.m_PlayerStationActionsCallbackInterface.OnFire;
                    @Fire.performed -= m_Wrapper.m_PlayerStationActionsCallbackInterface.OnFire;
                    @Fire.canceled -= m_Wrapper.m_PlayerStationActionsCallbackInterface.OnFire;
                }
                m_Wrapper.m_PlayerStationActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Fire.started += instance.OnFire;
                    @Fire.performed += instance.OnFire;
                    @Fire.canceled += instance.OnFire;
                }
            }
        }
        public PlayerStationActions @PlayerStation => new PlayerStationActions(this);
        private int m_MouseKbSchemeIndex = -1;
        public InputControlScheme MouseKbScheme
        {
            get
            {
                if (m_MouseKbSchemeIndex == -1) m_MouseKbSchemeIndex = asset.FindControlSchemeIndex("MouseKb");
                return asset.controlSchemes[m_MouseKbSchemeIndex];
            }
        }
        public interface IEditorActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnLeftClick(InputAction.CallbackContext context);
            void OnRightClick(InputAction.CallbackContext context);
            void OnRotateLeft(InputAction.CallbackContext context);
            void OnRotateRight(InputAction.CallbackContext context);
            void OnToggleVerticalMirror(InputAction.CallbackContext context);
            void OnZoom(InputAction.CallbackContext context);
        }
        public interface IDebugActions
        {
            void OnToggleConsole(InputAction.CallbackContext context);
            void OnDrag(InputAction.CallbackContext context);
            void OnRotate(InputAction.CallbackContext context);
        }
        public interface IReturnNavigateableActions
        {
            void OnReturn(InputAction.CallbackContext context);
        }
        public interface IGameplayActions
        {
            void OnZoom(InputAction.CallbackContext context);
            void OnRightClick(InputAction.CallbackContext context);
            void OnLeftClick(InputAction.CallbackContext context);
            void OnMovement(InputAction.CallbackContext context);
            void OnEscape(InputAction.CallbackContext context);
            void OnSaveGroup(InputAction.CallbackContext context);
            void OnSelectGroup(InputAction.CallbackContext context);
            void OnSaveGroupModifier(InputAction.CallbackContext context);
        }
        public interface IPlayerStationActions
        {
            void OnFire(InputAction.CallbackContext context);
        }
    }
}
