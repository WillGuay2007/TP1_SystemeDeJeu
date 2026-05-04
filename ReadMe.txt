Questions au choix (j'ai tout fait sans faire exprès) :

Système que j'aurais aimé avoir : un système flexible de destruction d'objets (comme des bombes, de la glace, etc. avec des comportements différents lors de la destruction à l'aide d'interfaces).

Ce que j'ai appris : j'ai appris comment séparer les systèmes différents en contrôleurs qui ont chacun une seule responsabilité.

Ce qui pourrait être amélioré : les exemples de code pourraient être plus clairs. Les noms de variables sont parfois trompeurs, comme dans l'exemple des windows où tu as fait un dictionnaire avec un nom LayerToWindow, mais ça aurait dû être WindowToLayer.

Question bonus :

Pour le projet pratique, j'essayerai de me faire un contrôleur de sauvegarde centralisé avec une fonction Save(ISaveable). Je ne sais pas trop si c'est possible de faire en sorte que la méthode Save() marcherait avec n'importe quoi, mais j'essayerai tout de même d'incorporer l'interface ISaveable avec une méthode Save() et je passerai cette interface au controller. C'est simple, mais selon moi ça devrait être quand même efficace. Le premier système qui devrait être sauvegardé devrait être celui d'inventaire et le deuxième devrait être celui de checkpoints. (Pour mon projet pratique qui n'est pas dans ce cours)