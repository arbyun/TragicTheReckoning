# Tragic: The Reckoning
Project for LP1 based on Magic: The Gathering

Trabalho feito por 
- Francisco Anjos(a22204855) : Markdown, UML, cartas e correção de código
- Daniela Dantas(a22202104): Deck, sistema de ataque, card handler, main e viewer

Este projeto foi-nos proposto como o segundo projeto para a avaliação da disciplina liguagens de programação 1.

O projeto consiste num jogo de cartas entre 2 jogadores, em que cada jogador começa com 6 cartas na mão.

O jogo começa por cada jogador a escolher que cartas quer jogar com a mana que tem disponível.

**Mecânicas**

 - Cada jogador começa com 6 cartas na mão.
 - Cada jogador começa o jogo com 1 de mana.
 - A mana cresce consequente ao turno que é.
 - A partir do turno 5, em qualquer turno seguinte, cada jogador terá sempre 5 mana para jogar.
 - Cada jogador agarra numa carta no inicio do seu turno.
 - Cada jogador tem 10 pontos de vida.

**Flow do jogo**

1. Cada jogador escreve o seu nome.
2. Cada jogador recebe uma mão de 6 cartas.
3. A mana respetiva a cada jogador é dada.
4. Cada jogador joga 1 ou mais cartas até não ter mana.
5. Quando cada jogador tiver escolhido as cartas a jogar, ambos passam para o combate.
6. Durante o combate, as cartas de ambos jogadores atacam.
7. Se houver dano para ser dado, será dado para a próxima carta no campo.
8. Caso não haja nenhuma carta para dar dano, o dano que falta dar será dado ao jogador/dono das cartas que morreram.
9. Continua-se o jogo até um jogador ficar sem vida ou sem mais cartas disponíveis no seu *deck*.

**Baralho**

|Name|Custo|Attack|Defense| Quantity |
|-------|--------|-----|-----|-------|
|Flying Wand|1|1|1|4|
|Severed Monkey Head|1|2|1|4|
|Mystical Rock Wall|2|0|5|2|
|Lobster McCrabs|2|1|3|2|
|Goblin Troll|3|3|2|2|
|Scortching Heatwave|4|5|3|1|
|Blind Minotaur|3|1|3|1|
|Tim, The Wizard|5|6|4|1|
|Sharply Dressed|4|3|3|1|
|Blue Steel|2|2|2|2|

**Referências**

Durante este projeto houve várias trocas de ideias entre os membros do grupo, nomeadamente de: como fazer a estrutura do jogo, como representar os jogadores e finalmente como deveriamos ter o viewer estruturado.

Nenhuma linha de script foi copiada, porém, foi pedido ao chatGPT ideias de como estruturar correctamente o método PhaseTwo() da classe singleton TurnHandler, sendo que nos respondeu com uma outline simplificada em escrito (guia) em pseudo-código.
