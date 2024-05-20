using System.Collections.Generic;
using Model.Runtime.Projectiles;
using UnityEngine;

namespace UnitBrains.Player
{
    public class SecondUnitBrain : DefaultPlayerUnitBrain
    {
        public override string TargetUnitName => "Cobra Commando";
        private const float OverheatTemperature = 3f;
        private const float OverheatCooldown = 2f;
        private float _temperature = 0f;
        private float _cooldownTime = 0f;
        private bool _overheated;

        protected override void GenerateProjectiles(Vector2Int forTarget, List<BaseProjectile> intoList)
        {
            float overheatTemperature = OverheatTemperature;
            ///////////////////////////////////////
            // Homework 1.3 (1st block, 3rd module)
            ///////////////////////////////////////           

            /*/
            Versiya 2 (nachalo)
            /*/
            
            int PTemperatura = GetTemperature(); // Uznat' temperaturu (iz metoda) i zapisat' ee (v peremennuyu).

            if (PTemperatura >= OverheatTemperature) return; // Proverit', peregrelos' li oruzhie.

            for (int i = 0; i <= PTemperatura; i++) // Ot kolichestva tsiklov zavisit kolichestvo snaryadov v odnom vystrele.
            {
                var projectile = CreateProjectile(forTarget);
                AddProjectileToList(projectile, intoList);
            }

            IncreaseTemperature(); // Posle kazhdogo vystrela uvelichit' temperaturu.

            // Debug.Log($"Temperatura pered vystrelom: {PTemperatura}. Kolichestvo snaryadov v vystrele: {PTemperatura + 1}");
            
            /*/
            Versiya 2 (konets)
            /*/

            /*/
            Versiya 1 (nachalo)
            /*/

            /*/
            GetTemperature();

            if (_temperature > OverheatTemperature)
            {
                Debug.Log("_temperature > OverheatTemperature (ostyvanie).");
            }
            
            else
            {
                int PNomerVystrela;

                if (_temperature < 1)
                {
                    PNomerVystrela = 1;
                }
                else if (_temperature < 2)
                {
                    PNomerVystrela = 2;
                }
                else
                {
                    PNomerVystrela = 3;
                }

                void MVyletOdnogoSnaryada()
                {
                    var projectile = CreateProjectile(forTarget);
                    AddProjectileToList(projectile, intoList);
                }

                switch (PNomerVystrela)
                {
                    case 1:
                        Debug.Log("Temperatura pered vystrelom:");
                        Debug.Log(_temperature);
                        Debug.Log("Kolichestvo snaryadov v vystrele: 1.");
                        MVyletOdnogoSnaryada();
                        IncreaseTemperature();
                        Debug.Log("Temperatura posle vystrela:");
                        Debug.Log(_temperature);
                        Debug.Log("---------------------------------------------------------------");
                        break;
                    case 2:
                        Debug.Log("Temperatura pered vystrelom:");
                        Debug.Log(_temperature);
                        Debug.Log("Kolichestvo snaryadov v vystrele: 2.");
                        MVyletOdnogoSnaryada();
                        MVyletOdnogoSnaryada();
                        IncreaseTemperature();
                        Debug.Log("Temperatura posle vystrela:");
                        Debug.Log(_temperature);
                        Debug.Log("---------------------------------------------------------------");
                        break;
                    case 3:
                        Debug.Log("Temperatura pered vystrelom:");
                        Debug.Log(_temperature);
                        Debug.Log("Kolichestvo snaryadov v vystrele: 3.");
                        MVyletOdnogoSnaryada();
                        MVyletOdnogoSnaryada();
                        MVyletOdnogoSnaryada();
                        IncreaseTemperature();
                        Debug.Log("Temperatura posle vystrela:");
                        Debug.Log(_temperature);
                        Debug.Log("---------------------------------------------------------------");
                        break;  
                    default:
                        Debug.Log("Default (vystrela net).");
                        break;
                }                
            }
            /*/

            /*/
            Versiya 1 (konets)
            /*/

            ///////////////////////////////////////
        }

        public override Vector2Int GetNextStep()
        {
            return base.GetNextStep();
        }

        protected override List<Vector2Int> SelectTargets()
        {
            ///////////////////////////////////////
            // Homework 1.4 (1st block, 4rd module)
            ///////////////////////////////////////
            List<Vector2Int> result = GetReachableTargets();

            // * * *

            float pTekuscheeNaimensheeRasstoyanie = float.MaxValue;

            Vector2Int pBlizhajshayaTsel = Vector2Int.zero;

            foreach (Vector2Int i in result)
            {
                float pRasstoyanieDoBazy = DistanceToOwnBase(i);

                if (pRasstoyanieDoBazy < pTekuscheeNaimensheeRasstoyanie)
                {
                    pTekuscheeNaimensheeRasstoyanie = pRasstoyanieDoBazy;
                    pBlizhajshayaTsel = i;
                }

            }
            // Debug.Log(pTekuscheeNaimensheeRasstoyanie);
            
            if (pBlizhajshayaTsel != Vector2Int.zero)
            {
                Debug.Log($"_________________________ Koordinaty blizhajshej tseli: {pBlizhajshayaTsel}.");
                result.Clear();
                result.Add(pBlizhajshayaTsel);
                Debug.Log("_________________________ Blizhajshaya tsel' vybrana.");
            }
            
            /*/
            while (result.Count > 1)
            {
                result.RemoveAt(result.Count - 1);
            }
            /*/            

            // * * *

            return result;
            ///////////////////////////////////////
        }

        public override void Update(float deltaTime, float time)
        {
            if (_overheated)
            {
                _cooldownTime += Time.deltaTime;
                float t = _cooldownTime / (OverheatCooldown / 10);
                _temperature = Mathf.Lerp(OverheatTemperature, 0, t);
                if (t >= 1)
                {
                    _cooldownTime = 0;
                    _overheated = false;
                }
            }
        }

        private int GetTemperature()
        {
            if (_overheated) return (int)OverheatTemperature;
            else return (int)_temperature;
        }

        private void IncreaseTemperature()
        {
            _temperature += 1f;
            if (_temperature >= OverheatTemperature) _overheated = true;
        }
    }
}