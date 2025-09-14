from PIL import Image

image = Image.open("image.jpg")

image.thumbnail((128, 64), Image.LANCZOS)
image.convert('RGB')

# Bildgröße abrufen
width, height = image.size

print(f"Breite: {width}, Höhe: {height}")

with open("rbg-array.txt", "w") as f:
    f.write(f"Breite(x): {width:03} Höhe(y):{height:03};{width};{height};\n")
    # Farbwerte jedes Pixels ausgeben
    for x in range(width):
        for y in range(height):
            r, g, b = image.getpixel((x, y))
            f.write(f"{x:03},{y:03},{r:03},{g:03},{b:03},\n")
            #print(f"Pixel an Position ({x}, {y}): Rot={r}, Grün={g}, Blau={b}")
