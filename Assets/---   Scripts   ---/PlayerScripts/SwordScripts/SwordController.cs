using ______Scripts______.PlayerScripts.Player;
using UnityEngine;

namespace ______Scripts______.PlayerScripts.SwordScripts
{
    public class SwordController : MonoBehaviour
    {
        private GameObject Player;

        private SwordScript __SwordScript;
        private PlayerAnimations _playerAnimations;

        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _audioClipAttack1;
        [SerializeField] private AudioClip _audioClipAttack2;
        [SerializeField] private AudioClip _audioClipAttack3;
        [SerializeField] private AudioClip _audioClipAll1;
        [SerializeField] private AudioClip _audioClipAll2;
        
        private void Awake()
        {
            Player = this.gameObject;
            __SwordScript = Player.GetComponent<SwordScript>();
            _playerAnimations = Player.GetComponent<PlayerAnimations>();
        }

        public void SwordAttack1()
        {
            _audioSource.PlayOneShot(_audioClipAttack1);
            __SwordScript.SwordAttack(0.8f, 0.6f, false, 0.8f, true);
            // StartCoroutine(_playerAnimations.SwordAnimations("isUseSwordAttack1"));
            _playerAnimations.ChangeAnimationState("Attack_1");
        }

        public void SwordAttack2()
        {
            _audioSource.PlayOneShot(_audioClipAttack2);
            __SwordScript.SwordAttack(1.2f, 0.7f, false, 0.8f, true);
            // StartCoroutine(_playerAnimations.SwordAnimations("isUseSwordAttack2"));
            _playerAnimations.ChangeAnimationState("Attack_2");
        }

        public void SwordAttack3()
        {
            _audioSource.PlayOneShot(_audioClipAttack3);
            __SwordScript.SwordAttack(1.5f, 0.8f, true, 0.8f, false);
            // StartCoroutine(_playerAnimations.SwordAnimations("isUseSwordAttack3"));
            _playerAnimations.ChangeAnimationState("Attack_3_4");
        }

        public void HittingAll1()
        {
            _audioSource.PlayOneShot(_audioClipAll1);
            __SwordScript.SwordAttack(0.8f, 0.6f, true, 0.5f, false);
            // StartCoroutine(_playerAnimations.SwordAnimations("isUseSwordAttack4"));
            _playerAnimations.ChangeAnimationState("Attack_3_4");
        }

        public void HittingAll2()
        {
            _audioSource.PlayOneShot(_audioClipAll2);
            __SwordScript.SwordAttack(1.2f, 1.5f, false, 0.5f, false);
            // StartCoroutine(_playerAnimations.SwordAnimations("isUseSwordAttack5"));
            _playerAnimations.ChangeAnimationState("Attack_5");
        }

        public void ArmorFrameAttack()
        {
            __SwordScript.SwordAttack(2f, 1f, true, 0.5f, false);
            // StartCoroutine(_playerAnimations.SwordAnimations("isUseSwordAttack4"));
        }
    }
}