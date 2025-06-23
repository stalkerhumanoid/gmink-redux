#define ink_social
if (async_load[? "type"] == "script") {
	var a = async_load;
	var script = a[?"script"];
	if (ds_map_exists(async_load, "7")) {
		script_execute(script, a[?"0"], a[?"1"], a[?"2"], a[?"3"], a[?"4"], a[?"5"], a[?"6"], a[?"7"]); 
	} else if (ds_map_exists(async_load, "6")) {
		script_execute(script, a[?"0"], a[?"1"], a[?"2"], a[?"3"], a[?"4"], a[?"5"], a[?"6"]); 
	} else if (ds_map_exists(async_load, "5")) {
		script_execute(script, a[?"0"], a[?"1"], a[?"2"], a[?"3"], a[?"4"], a[?"5"]); 
	} else if (ds_map_exists(async_load, "4")) {
		script_execute(script, a[?"0"], a[?"1"], a[?"2"], a[?"3"], a[?"4"]); 
	} else if (ds_map_exists(async_load, "3")) {
		script_execute(script, a[?"0"], a[?"1"], a[?"2"], a[?"3"]); 
	} else if (ds_map_exists(async_load, "2")) {
		script_execute(script, a[?"0"], a[?"1"], a[?"2"]); 
	} else if (ds_map_exists(async_load, "1")) {
		script_execute(script, a[?"0"], a[?"1"]); 
	} else if (ds_map_exists(async_load, "0")) {
		script_execute(script, a[?"0"]); 
	} else {
		script_execute(script);
	}
}
