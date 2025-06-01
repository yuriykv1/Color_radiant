using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public class GhostScript : MonoBehaviour
    {
        private Animator Anim;
        private CharacterController Ctrl;
        private Vector3 MoveDirection = Vector3.zero;

        private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
        private static readonly int MoveState = Animator.StringToHash("Base Layer.move");
        private static readonly int SurprisedState = Animator.StringToHash("Base Layer.surprised");
        private static readonly int AttackState = Animator.StringToHash("Base Layer.attack_shift");
        private static readonly int DissolveState = Animator.StringToHash("Base Layer.dissolve");
        private static readonly int AttackTag = Animator.StringToHash("Attack");

        [SerializeField] private SkinnedMeshRenderer[] MeshR;
        private float Dissolve_value = 1;
        private bool DissolveFlg = false;
        private const int maxHP = 3;
        private int HP = maxHP;

        [SerializeField] private float Speed = 4;
        [SerializeField] private Transform player;
        [SerializeField] private float detectionRange = 10f;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float attackCooldown = 2f;
        private float lastAttackTime = 0f;

        private const int Dissolve = 1;
        private const int Attack = 2;
        private const int Surprised = 3;
        private Dictionary<int, bool> PlayerStatus = new Dictionary<int, bool>
        {
            {Dissolve, false },
            {Attack, false },
            {Surprised, false },
        };

        void Start()
        {
            Anim = GetComponent<Animator>();
            Ctrl = GetComponent<CharacterController>();

            if (player == null)
            {
                GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
                if (foundPlayer != null)
                    player = foundPlayer.transform;
            }
        }

        void Update()
        {
            STATUS();
            GRAVITY();

            if (!PlayerStatus.ContainsValue(true))
            {
                MOVE();
            }
            else if (PlayerStatus[Dissolve])
            {
                PlayerDissolve();
            }

            if (HP <= 0 && !DissolveFlg)
            {
                Anim.CrossFade(DissolveState, 0.1f, 0, 0);
                DissolveFlg = true;
                StartCoroutine(DestroyAfterDelay(2f)); // Удаляем врага через 2 секунды
            }
        }

        private void STATUS()
        {
            PlayerStatus[Dissolve] = DissolveFlg && HP <= 0;
            PlayerStatus[Attack] = Anim.GetCurrentAnimatorStateInfo(0).tagHash == AttackTag;
            PlayerStatus[Surprised] = Anim.GetCurrentAnimatorStateInfo(0).fullPathHash == SurprisedState;
        }

        private void PlayerDissolve()
        {
            Dissolve_value -= Time.deltaTime;
            foreach (var mesh in MeshR)
            {
                mesh.material.SetFloat("_Dissolve", Dissolve_value);
            }
            if (Dissolve_value <= 0)
            {
                Ctrl.enabled = false;
            }
        }

        private void TryAttack()
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;
                Anim.CrossFade(AttackState, 0.1f, 0, 0);
                // здесь можешь добавить урон игроку
            }
        }

        private void MOVE()
        {
            if (player == null || HP <= 0) return;

            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= attackRange)
            {
                TryAttack();
            }
            else if (distance <= detectionRange)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                direction.y = 0;
                MoveDirection = direction * Speed;
                if (Ctrl.enabled)
                {
                    Ctrl.Move(MoveDirection * Time.deltaTime);
                }
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

                if (Anim.GetCurrentAnimatorStateInfo(0).fullPathHash != MoveState)
                    Anim.CrossFade(MoveState, 0.1f, 0, 0);
            }
            else
            {
                if (Anim.GetCurrentAnimatorStateInfo(0).fullPathHash != IdleState)
                    Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }

        private void GRAVITY()
        {
            if (Ctrl.enabled)
            {
                if (CheckGrounded())
                {
                    if (MoveDirection.y < -0.1f)
                    {
                        MoveDirection.y = -0.1f;
                    }
                }
                MoveDirection.y -= 0.1f;
                Ctrl.Move(MoveDirection * Time.deltaTime);
            }
        }

        private bool CheckGrounded()
        {
            if (Ctrl.isGrounded && Ctrl.enabled) return true;
            Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
            return Physics.Raycast(ray, 0.2f);
        }

        public void TakeDamage(int amount)
        {
            if (HP <= 0) return;
            Anim.CrossFade(SurprisedState, 0.1f, 0, 0);
            HP -= amount;
        }

        private IEnumerator DestroyAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
}
