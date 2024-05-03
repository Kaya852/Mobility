class Multiplier {
public:
	Multiplier();
	int multiply(int x, int y);

};

extern "C" __declspec(dllexport) void* Create() {
	return (void*) new Multiplier();
}

extern "C" __declspec(dllexport) int multiply(Multiplier * a, int x, int y) {
	return a->multiply(x, y);
}