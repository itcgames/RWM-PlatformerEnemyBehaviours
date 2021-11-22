using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class FollowerTest
    {
        private GameObject Player;
        private GameObject Enemy;

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
        public IEnumerator StartPhaseTest()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            Enemy = GameObject.FindGameObjectWithTag("Follower");
            float initialHealth = Enemy.GetComponent<FlyingFollower>().getHealth();
            Enemy.GetComponent<FlyingFollower>().invincible = true;
            Enemy.GetComponent<FlyingFollower>().damage(2);
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(Enemy.GetComponent<FlyingFollower>().getHealth(), initialHealth);
        }

        [UnityTest]
        public IEnumerator StateChange()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            Enemy = GameObject.FindGameObjectWithTag("Follower");
            bool initialstate = Enemy.GetComponent<FlyingFollower>().invincible;
            Player.GetComponent<Rigidbody2D>().position = new Vector2(Enemy.GetComponent<Rigidbody2D>().position.x - 2, Enemy.GetComponent<Rigidbody2D>().position.y - 5);
            yield return new WaitForSeconds(0.5f);
            Assert.AreNotEqual(Enemy.GetComponent<FlyingFollower>().invincible, initialstate);
        }

        [UnityTest]
        public IEnumerator TakeDamage()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            Enemy = GameObject.FindGameObjectWithTag("Follower");
            float initialHealth = Enemy.GetComponent<FlyingFollower>().getHealth();
            Enemy.GetComponent<FlyingFollower>().invincible = false;
            Enemy.GetComponent<FlyingFollower>().damage(2);
            yield return new WaitForSeconds(0.1f);
            Assert.Less(Enemy.GetComponent<FlyingFollower>().getHealth(), initialHealth);
        }
    }
}
