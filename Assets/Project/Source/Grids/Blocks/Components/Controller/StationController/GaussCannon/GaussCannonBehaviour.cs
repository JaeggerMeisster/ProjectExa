using DG.Tweening;
using Exa.Math;
using Exa.Utils;
using Exa.VFX;
using UnityEngine;

#pragma warning disable 649

namespace Exa.Grids.Blocks.Components {
    public class GaussCannonBehaviour : ChargeableTurretBehaviour<GaussCannonData> {
        [Header("References")]
        [SerializeField] private Animator animator;
        [SerializeField] private Transform beamOrigin;
        [SerializeField] private GaussCannonArcs arcs;
        [SerializeField] private LineRenderer lineRenderer;

        public override bool CanResumeCharge {
            get => true;
        }

        public override void StartCharge() {
            base.StartCharge();
            animator.Play("Charge", 0, GetNormalizedChargeProgress());
        }

        public override void EndCharge() {
            if (charging) {
                animator.Play("CancelCharge", 0, 1f - GetNormalizedChargeProgress());
            }

            base.EndCharge();
        }

        protected override void BlockUpdate() {
            var prevChargeTime = chargeTime;
            
            base.BlockUpdate();

            if (prevChargeTime != chargeTime) {
                arcs.SetChargeProgress(GetNormalizedChargeProgress());
            } 
        }

        public override void Fire() {
            var endPoint = HitScanFire(Data.damage, Data.Range, beamOrigin);

            lineRenderer.SetPosition(0, beamOrigin.position.SetZ(-0.2f));
            lineRenderer.SetPosition(1, endPoint.SetZ(-0.2f));

            lineRenderer.gameObject.SetActive(true);
            lineRenderer.widthMultiplier = 1.5f;
            
            lineRenderer.DOWidthMultiplier(0.5f, 0.5f)
                .OnComplete(() => lineRenderer.gameObject.SetActive(false));
        }

        protected override void OnAdd() {
            base.OnAdd();
            var chargeSpeed = 1f / Data.chargeTime;
            animator.SetFloat("ChargeSpeed", chargeSpeed);
            animator.SetFloat("ChargeDecaySpeed", chargeSpeed * chargeDecaySpeed);

            arcs.RandomizeMaterials();
        }
    }
}