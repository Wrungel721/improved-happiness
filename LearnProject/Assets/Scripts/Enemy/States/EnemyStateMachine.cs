using System;
using System.Collections.Generic;
using LearnProject.FSM;
using Unity.VisualScripting.Dependencies.NCalc;

namespace LearnProject.Enemy.States
{
    public class EnemyStateMachine : BaseStateMachine
    {
        private const float NavMeshTurnOffDistance = 5;
        Random rnd = new Random();
        public EnemyStateMachine(EnemyDirectionController enemyDirectionController, EnemyCharacter enemyCharacter,
            NavMesher navMesher, EnemyTarget target) 
        {
            var idleState = new IdleState();
            var findWayState = new FindWayState(target, navMesher, enemyDirectionController);
            var moveForwardState = new MoveForwardState(target, enemyDirectionController);
            var escapeState = new EscapeState(target, enemyDirectionController, enemyCharacter);

            SetInitialState(idleState);

            AddState(state: idleState, transitions: new List<Transition>
            {
                new Transition(
                    findWayState,
                    () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                new Transition(
                    moveForwardState,
                    () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance),
                new Transition(
                    escapeState,
                    () => (enemyCharacter.health < enemyCharacter.boarderHealth)&&(rnd.Next(0, 100) < enemyCharacter._braveness) ),
            }) ;

            AddState(state: findWayState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => target.Closest == null),
                new Transition(
                    moveForwardState,
                    () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance)
            });

            AddState(state: moveForwardState, transitions: new List<Transition>
            {
                new Transition(
                    findWayState,
                    () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                new Transition(
                    escapeState,
                    () => (enemyCharacter.health < enemyCharacter.boarderHealth)&&(rnd.Next(0, 100) < enemyCharacter._braveness)),
                new Transition(
                    idleState,
                    () => target.Closest == null)
            });

            AddState(state: escapeState, transitions: new List<Transition>
            {
                new Transition(
                    idleState,
                    () => target.Closest == null)
            });


        }

        private bool Next(int v1, int v2)
        {
            throw new NotImplementedException();
        }
    }
}
