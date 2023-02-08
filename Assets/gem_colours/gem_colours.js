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
        var HEX = hsvToHex(H, S, 1);

        console.log(`Level${level}_${colour}: ${H} ${S} 1 ${HEX}`);
    }
}

function hsvToHex(h, s, v) {
  let r, g, b;

  let i = Math.floor(h * 6);
  let f = h * 6 - i;
  let p = v * (1 - s);
  let q = v * (1 - f * s);
  let t = v * (1 - (1 - f) * s);

  switch (i % 6) {
    case 0: r = v, g = t, b = p; break;
    case 1: r = q, g = v, b = p; break;
    case 2: r = p, g = v, b = t; break;
    case 3: r = p, g = q, b = v; break;
    case 4: r = t, g = p, b = v; break;
    case 5: r = v, g = p, b = q; break;
  }

  let hexR = Math.round(r * 255).toString(16).padStart(2, '0');
  let hexG = Math.round(g * 255).toString(16).padStart(2, '0');
  let hexB = Math.round(b * 255).toString(16).padStart(2, '0');

  return `#${hexR}${hexG}${hexB}`;
}