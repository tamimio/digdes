alphabet = 'abcdefghijklmnopqrstuvwxyz'
symb=' .,!?:;"-+=)(*&^%$#@[]{}\/|'

offset = int (input ("Input offset -> "))
orig_txt = input("Input text -> ")
orig_txt=orig_txt.lower()
enc_txt=""
dec_txt=""
print("Offset: ", offset)
print ("Original text: ", orig_txt)

for i in range(0, len(orig_txt)):
   if symb.find(orig_txt[i]) == -1 : # comment this line to encrypt w/ saving spaces and other symbols (as in example)
        index = (alphabet.find(orig_txt[i]) + offset) % (len(alphabet)-1)
        enc_txt = enc_txt[:i] + alphabet[index] + enc_txt[i+1:]
    
print("Encrypted text: ", enc_txt)

for i in range(0, len(enc_txt)):
    index = (alphabet.find(enc_txt[i]) - offset) % (len(alphabet)-1)
    dec_txt = dec_txt[:i] + alphabet[index] + dec_txt[i+1:]
    
print("Decrypted text: ", dec_txt)
    
