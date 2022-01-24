using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class BomberTest
    {
        private GameObject Player;
        private GameObject Enemy;
        private GameObject bomb;
        private GameObject shrapnel;

        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        }

        [TearDown]
        public void Teardown()
        {
            SceneManager.UnloadSceneAsync("TestScene");
        }

        [UnityTest]
        public IEnumerator Movement()
        {

            Enemy = GameObject.FindGameObjectWithTag("Bomber");
            Vector2 initialPos = Enemy.GetComponent<Rigidbody2D>().position;
            yield return new WaitForSeconds(0.5f);
            Assert.Less(Enemy.GetComponent<Rigidbody2D>().position.x, initialPos.x);
        }
    }
}
