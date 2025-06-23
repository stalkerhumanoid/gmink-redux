/// @description Insert description here
// You can write your code in this editor
if (!choice) {
	if (ink_can_continue()) {
		text = ink_continue();
	} else {
		var choice_count = ink_choice_count();
		if (choice_count > 0) {
			choice = true;
			var choice_string = "";
			for(var i = 0; i < choice_count; i++) {
				choice_string += string(i) + ": " + ink_choice(i) + "\n";
			}
			
			text = choice_string;
		}
	}
} else {
	var _y = (room_height / 2) - (string_height(text) / 2);
	var c = (mouse_y - _y) div string_height("W");
	
	show_debug_message(c);
	
	if (c >= 0 and c < ink_choice_count()) {
		ink_choose_choice(c);
		text = ink_continue();
		choice = false;
	}
}