gcc -shared -fPIC $(python3-config --cflags) PythonPlugin.c -o pythonscripting.so $(python3-config --ldflags)