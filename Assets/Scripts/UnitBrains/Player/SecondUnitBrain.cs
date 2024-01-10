using System;
using System.Collections.Generic;
using Model.Runtime.Projectiles;
using Unity.VisualScripting;
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


        ///////////////////////////////////////
        // Homework 1.3 (1st block, 3rd module)
        ///////////////////////////////////////   
        protected override void GenerateProjectiles(Vector2Int forTarget, List<BaseProjectile> intoList)
        {


            float overheatTemperature = OverheatTemperature;

            // Реализуй механику нагрева оружия.
            // a.Тебе необходимо проверить, а не перегрелось ли уже оружие.
            // Для этого получи текущую температуру с помощью метода GetTemperature,
            // который уже реализован в данном скрипте. И если текущая температура,
            // возвращенная этим методом больше или равна температуре перегрева - overheatTemperature,
            // то выполнение метода следует прервать, до остывания оружия,
            // которое происходит автоматически.

            float currentTemperature = GetTemperature();

            while (true)
            {
                if (currentTemperature >= overheatTemperature)
                {
                    break;
                }

                // Реализуй механику увеличения снарядов с каждым выстрелом.
                // a.Обрати внимание на код в методе GenerateProjectiles, который
                // генерирует снаряды и добавляет их в некий лист.Тебе необходимо
                // обернуть его в цикл так, чтобы при каждом выстреле количество
                // снарядов соответствовало текущей температуре оружия.
                // b.Какой цикл для этого больше подходит - реши самостоятельно.
                else
                {
                    for (int i = 0; i < overheatTemperature; i++)
                    {
                        var projectile = CreateProjectile(forTarget);
                        AddProjectileToList(projectile, intoList);

                        // b.Каждый вызов метода GenerateProjectiles соответствует одному выстрелу.
                        // С каждым выстрелом нагрев оружия должен увеличиваться.Для этого используй
                        // метод IncreaseTemperature, он также уже реализован в данном скрипте, тебе
                        // необходимо лишь его вызвать.

                        IncreaseTemperature();
                    }
                }
            }

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
            while (result.Count > 1)
            {
                result.RemoveAt(result.Count - 1);
            }
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