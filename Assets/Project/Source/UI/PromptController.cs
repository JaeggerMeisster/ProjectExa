using System;
using Exa.UI.Components;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649

namespace Exa.UI {
    public class PromptController : MonoBehaviour {
        [SerializeField] private Transform ownerObject;
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;
        [SerializeField] private FormGenerator formGenerator;
        [SerializeField] private Transform yesNoContainer;
        [SerializeField] private Button okButton;
        [SerializeField] private Text promptText;
        [SerializeField] private ProgressBar progressBar;

        public Action<string> PromptTextSetter {
            get => value => promptText.text = value;
        }

        public Prompt PromptForm<T>(
            string message,
            IUIGroup uiGroup,
            ModelDescriptor<T> modelDescriptor,
            Action<T> onSubmit
        ) {
            var prompt = new Prompt(
                this,
                message,
                uiGroup,
                () => { okButton.onClick.RemoveAllListeners(); },
                new[] {
                    okButton.gameObject,
                    formGenerator.gameObject
                }
            );

            formGenerator.GenerateForm(modelDescriptor);

            okButton.onClick.AddListener(
                () => {
                    onSubmit(modelDescriptor.FromDescriptor());
                    prompt.CleanUp();
                }
            );

            return prompt;
        }

        public Prompt PromptYesNo(string message, IUIGroup uiGroup, Action<bool> onClosePrompt = null) {
            var prompt = new Prompt(
                this,
                message,
                uiGroup,
                () => {
                    yesButton.onClick.RemoveAllListeners();
                    noButton.onClick.RemoveAllListeners();
                },
                new[] {
                    yesNoContainer.gameObject
                }
            );

            yesButton.onClick.AddListener(
                () => {
                    onClosePrompt?.Invoke(true);
                    prompt.CleanUp();
                }
            );

            noButton.onClick.AddListener(
                () => {
                    onClosePrompt?.Invoke(false);
                    prompt.CleanUp();
                }
            );

            return prompt;
        }

        public Prompt PromptOk(string message, IUIGroup uiGroup, Action onClosePrompt = null) {
            var prompt = new Prompt(
                this,
                message,
                uiGroup,
                () => { okButton.onClick.RemoveAllListeners(); },
                new[] {
                    okButton.gameObject
                }
            );

            okButton.onClick.AddListener(
                () => {
                    onClosePrompt?.Invoke();
                    prompt.CleanUp();
                }
            );

            return prompt;
        }

        public IProgress<float> PromptProgress(string message, IUIGroup uiGroup) {
            var prompt = null as Prompt;

            return new Progress<float>(
                value => {
                    if (value == 0f) {
                        prompt = new Prompt(
                            this,
                            message,
                            uiGroup,
                            () => { progressBar.Report(0f); },
                            new[] {
                                progressBar.gameObject
                            }
                        );
                    }

                    if (value == 1f) {
                        prompt.CleanUp();
                    }

                    progressBar.Report(value);
                }
            );
        }

        internal void ActivateMessage(string message, IUIGroup uiGroup) {
            ownerObject.gameObject.SetActive(true);
            uiGroup.Interactable = false;
            promptText.text = message;
        }

        internal void DeactivateMessage(IUIGroup uiGroup) {
            ownerObject.gameObject.SetActive(false);
            uiGroup.Interactable = true;
        }
    }
}