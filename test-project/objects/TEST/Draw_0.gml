/// @description Insert description here
// You can write your code in this editor
draw_set_halign(fa_left);
draw_set_valign(fa_top);
draw_text(0, 0, "health: " + string(health));

if (shaking) {
	var s = irandom_range(-3, 3);
} else {
	s = 0;
}

draw_set_halign(fa_center);
draw_set_valign(fa_middle);
draw_text(room_width/2 + s, room_height/2, text);

draw_set_halign(fa_left);
draw_set_valign(fa_bottom);
draw_text(0, room_height, "Press H recieve 50 health!");