# TODO - Correction Sidebar (Permissions + UI interactive)

## Problème
- Avec `@rendermode(prerender: false)` sur `<Aside>` → permissions OK mais sidebar figée (JS perdu)
- Sans render mode → sidebar fonctionne mais permissions vides

## Étapes

- [x] Étape 1 : Analyser les fichiers et comprendre le problème
- [x] Étape 2 : Corriger `Aside.razor.cs` — Garder `OnInitializedAsync` pour les permissions + `OnAfterRenderAsync` pour JS (avec try/catch + log)
- [x] Étape 3 : Vérifier `MainLayout.razor` — Render mode déjà présent (inchangé)
- [x] Étape 4 : Corriger `wwwroot/js/blazor-init.js` — Améliorer `reinitUi` :
  - ✅ Détruit proprement l'ancienne instance SimpleBar
  - ✅ Nettoie les artefacts DOM laissés par SimpleBar
  - ✅ Recrée une nouvelle instance SimpleBar
  - ✅ Ré-attache les événements de clic pour les menus déroulants (niveau 1 et interne)
  - ✅ Ré-applique les classes actives (`setNavActive`)
  - ✅ Ré-initialise la bascule sidebar (`toggleSidemenu`)
- [x] Étape 5 : Corrections terminées, prêt à tester.

