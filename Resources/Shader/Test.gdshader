shader_type spatial;

uniform sampler2D BkTex:source_color;
uniform sampler2D CharaTex:source_color;
uniform sampler2D CharaTex2:source_color;
uniform vec3 Vec=vec3(0.0,0.0,0.0);
uniform vec3 Vect=vec3(0.0,0.0,0.0);

void vertex() {
	
	
}

void fragment() {
	vec3 p=Vec.z/dot(VIEW,NORMAL) *VIEW;
	vec4 pp=INV_PROJECTION_MATRIX*vec4(p,0.0)+vec4(Vec.xy,0.0,0.0);
	pp.x=-pp.x;
	
	vec3 p2=Vec.z/dot(VIEW,NORMAL) *VIEW;
	vec4 pp2=INV_PROJECTION_MATRIX*vec4(p2,0.0)+vec4(Vect.xy,0.0,0.0);
	pp2.x=-pp2.x;
	
	vec3 bkab=texture(BkTex,UV).rgb;
	float bka=texture(BkTex,UV).a;
	
	ALBEDO=texture(CharaTex,pp.xy+UV).rgb*(1.0-bka)+texture(CharaTex2,pp2.xy+UV).rgb
	*(1.0-texture(CharaTex,pp.xy+UV).a)*(1.0-bka)+bkab;
	
	ALPHA=texture(CharaTex,pp.xy+UV).a*(1.0-bka)+texture(CharaTex2,pp2.xy+UV).a
	*(1.0-texture(CharaTex,pp.xy+UV).a)*(1.0-bka)+bka;
}

//void light() {
	// Called for every pixel for every light affecting the material.
	// Uncomment to replace the default light processing function with this one.
//}
