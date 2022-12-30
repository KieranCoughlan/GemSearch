console.log('Gem Colours');

// Works out HSV colour for each gem
// H varies from 0 - 1 depending on number of colors per level
// S varies from 1 at level 0 to 0 at level 3
// V constant at 1

var numLevels = 4;

for (var level = numLevels - 1; level >= 0; level--)
{
    var numColours = Math.pow(2, 3 - level);
    
    var levelInvert = numLevels - 1 - level;
    var S = levelInvert / numLevels;

    for (var colour = numColours - 1; colour >= 0; colour--)
    {
        var H = (colour + 1) / numColours;

        console.log(`Level${level}_${colour}: ${H} ${S} 1`);
    }
}