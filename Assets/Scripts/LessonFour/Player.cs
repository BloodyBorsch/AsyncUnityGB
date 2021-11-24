using UnityEngine;
using UnityEngine.Networking;


namespace LessonFour
{
    public sealed class Player : NetworkBehaviour
    {
        [SerializeField]
        private GameObject playerPrefab;
        private GameObject playerCharacter;

        private void Start()
        {
            SpawnCharacter();
        }

        private void SpawnCharacter()
        {
            if (!isServer)
            {
                return;
            }

            playerCharacter = Instantiate(playerPrefab, transform);

            NetworkServer.SpawnWithClientAuthority(playerCharacter, connectionToClient);
        }
    }
}
