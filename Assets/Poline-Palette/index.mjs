

import { Poline } from 'poline';
import { formatHex, parse, converter } from 'culori';

var hsl_convertor = converter('hsl');
var rgb_convertor = converter('rgb');

var anchors_hex = ['#F40076', '#DEAE00', '#0030DE'];
//var anchors_hex = ['#FF0000', '#00FF00', '#0000FF'];
var anchors_rgb = anchors_hex.map(h => parse(h));
var anchors_hsl = anchors_rgb.map(c => hsl_convertor(c));
var anchors_hsl_plain = anchors_hsl.map(c => [c.h, c.s, c.l]);

console.info(anchors_hsl_plain);

const poline = new Poline({
  anchorColors: anchors_hsl_plain,
  numPoints: 1
});

console.info(poline.colors);

const OKHslColors = [...poline.colors].map(
  c => formatHex(rgb_convertor({
    mode: 'hsl',
    h: c[0],
    s: c[1],
    l: c[2]
  }))
);

OKHslColors.forEach(c => console.info(c));

