EXTERNAL shake()
VAR hp = 100
->start
==start==
You walk around the forest

->attack

==attack==
A {~Snake|Bear|Skeleton} {shows up| appears}!

->eTurn

==eTurn==
It attacks you! {~But Misses! ->choices | Ouch! ->hit}


==choices==
    + [drink potion]
        ~hp += 20
        {Thats better! | Feels good!}
        ->eTurn
    + [fight back!]
        {~Missed! | No good! | nope! | Got him! -> start | Its a hit! ->start}
        ->eTurn
        
    * [Use Ultimate weapon]
        You use the ultimate weapon. Healing you and obliterating the enemy.
        The ultimate weapon dissappears from your hands.
        ~hp += 100
        ->start
        
==hit==
~hp -= 15
~shake()
->choices

===function shake()===
~ return 0