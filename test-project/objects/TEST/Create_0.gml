ink_load(working_directory + "test.json");
ink_observe_variable("hp", health_changed);
ink_bind_external("shake", shake);
health = 100;

text = ink_continue();
choice = false;
shaking = false;