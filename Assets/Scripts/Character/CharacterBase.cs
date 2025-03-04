using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace INeverFall
{
    public abstract class CharacterBase : MonoBehaviour, IDamageable
    {
        protected int _maxHp;
        protected int _hp;
        protected int _attackPower;

        private bool _isDead;

        protected virtual void Start()
        {
            
        }

        public virtual void OnDamage(CharacterBase owner, int damage)
        {
            if (_isDead) return;
            
            _hp -= damage;
            _OnDamage();

            if (_hp <= 0)
            {
                _hp = 0;
                _isDead = true;
                _OnDead();
            }
            
            //Debug.Log(this.GetType().Name + " Hp : " + _hp);
        }

        protected virtual void _OnDead() { }
        protected virtual void _OnDamage() { }

        public int AttackPower => _attackPower;
    }
}