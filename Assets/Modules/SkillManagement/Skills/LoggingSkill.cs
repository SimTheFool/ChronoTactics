using System.Collections.Generic;
using System;

public class LoggingSkill : Skill
{
    public override void BuildSkill(SkillInput input)
    {
        Func<SkillProcess, SkillProcess> onDoubleWord_DisplayWord = (parentProcess) => {
            DoubleWordProcess comp = (DoubleWordProcess)parentProcess;
            return new DisplayWordProcess(comp.OutputWord);
        };

        Func<SkillProcess, SkillProcess> onDoubleWord_DoubleWord = (parentProcess) => {
            DoubleWordProcess comp = (DoubleWordProcess)parentProcess;
            return new DoubleWordProcess(comp.OutputWord);
        };


        SkillComposite composite = new SkillComposite((parentProcess) => {
            return new DoubleWordProcess("foo");
        });
        composite.Init();

        composite
        .In(onDoubleWord_DoubleWord)
            .Do(onDoubleWord_DisplayWord)
            .Do(onDoubleWord_DisplayWord)
        .Out()
        .Do(onDoubleWord_DisplayWord)
        .Do((parentProcess) => new DisplayWordProcess("brenda"))
        ;

        this.statePerComposites = new Dictionary<SkillComposite, bool>();
        this.statePerComposites.Add(composite, false);

        /* DoubleWordComposite doubleWord = new DoubleWordComposite();
        doubleWord.word = input.Caster.Name;

        doubleWord
            .Do<DisplayWordComposite>((parent, composite) => composite.wordToDisplay = parent.outputWord)
            .In<DoubleWordComposite>((parent, composite) => composite.word = "brenda")
                .Do<DisplayWordComposite>((parent, composite) => composite.wordToDisplay = parent.outputWord)
                .Do<DisplayWordComposite>((parent, composite) => composite.wordToDisplay = parent.outputWord)
            .Out()
        ;

        this.statePerComposites = new Dictionary<SkillComposite, bool>();
        this.statePerComposites.Add(doubleWord, false); */

        /*
        
        FindEnemies = new FindEnemiesComposite( filter, (composite) => {
            List<Push> Pushs = new List<Push>();
            foreach(Actor enemy in composite.enemies)
            {
                Pushs.Add(new Push(enemy), (composite) => {
                    return new FindNearbyTiles(enemy, (composite) => {
                        List<TileFacade> tiles = new List<TileFacade>();
                        foreach(TileFacade tile in composite.tiles)
                        {
                            tiles.Add(new Burn(tile));
                        }
                        return tiles;
                    })
                });
            }
            return pushs;
        })


        FindEnemiesComposite findEnemies = new FindEnemies(filter);
        findEnemies.Then(onEnemiesFound_Push);

        Action<SkillComposite> onEnemiesFound_Push = (composite) => {
            List<Push> Pushs = new List<Push>();
            foreach(Actor enemy in composite.enemies)
            {
                Push push = new Push(enemy);
                push.Then(onEnemyPush_FindNearbyTile);
                Pushs.Add(push);
            }
            return pushs;
        }

        Action<SkillComposite> onEnemyPush_FindNearbyTile = (composite) => {
            return new FindNearbyTiles(enemy);
            Then(onNearbyTilesFound_BurnTile);
        }

        Action<SkillComposite> onNearbyTilesFound_BurnTile = (composite) => {
            List<TileFacade> tiles = new List<TileFacade>();
                foreach(TileFacade tile in composite.tiles)
                {
                    tiles.Add(new Burn(tile));
                }
                return tiles;
        }
        
        
        findEnemies.Do(onEnemiesFound_Push).Do
        
        
        
         */
    }
}