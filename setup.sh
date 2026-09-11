#!/bin/bash

# ========================================
# GPA NOVEC - Setup Script (Mac/Linux)
# Exécuter: bash setup.sh ou ./setup.sh
# ========================================

echo "🚀 GPA NOVEC - Configuration Automatique"
echo "========================================"
echo ""

# Couleurs
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
CYAN='\033[0;36m'
NC='\033[0m' # No Color

# Vérifier .NET
echo -e "${YELLOW}✓ Vérification de .NET 8.0...${NC}"
DOTNET_VERSION=$(dotnet --version 2>/dev/null)
if [[ $DOTNET_VERSION == 8.* ]]; then
    echo -e "${GREEN}  ✅ .NET 8.0 trouvé: $DOTNET_VERSION${NC}"
else
    echo -e "${RED}  ❌ .NET 8.0 non trouvé!${NC}"
    echo -e "${YELLOW}  📥 Téléchargez depuis: https://dotnet.microsoft.com/download/dotnet/8.0${NC}"
    exit 1
fi

# Vérifier SQL Server (Docker ou Host)
echo ""
echo -e "${YELLOW}✓ Vérification de SQL Server...${NC}"
if command -v docker &> /dev/null; then
    echo -e "${GREEN}  ✅ Docker trouvé${NC}"
    echo -e "${CYAN}  💡 Suggestion: Utilisez SQL Server Docker${NC}"
    echo -e "${CYAN}  docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Test@12345' -p 1433:1433 -d mcr.microsoft.com/mssql/server:latest${NC}"
else
    echo -e "${YELLOW}  ⚠️  Docker non trouvé (optionnel)${NC}"
fi

# Restaurer les dépendances
echo ""
echo -e "${YELLOW}✓ Restauration des dépendances NuGet...${NC}"
dotnet restore
if [ $? -ne 0 ]; then
    echo -e "${RED}  ❌ Erreur lors de la restauration!${NC}"
    exit 1
fi
echo -e "${GREEN}  ✅ Dépendances restaurées${NC}"

# Créer la base de données
echo ""
echo -e "${YELLOW}✓ Création de la base de données...${NC}"

# Attendre que SQL Server soit prêt (si Docker)
if docker ps 2>/dev/null | grep -q mssql; then
    echo -e "${CYAN}  ⏳ Attente que SQL Server soit prêt...${NC}"
    sleep 10
fi

dotnet ef database update
if [ $? -ne 0 ]; then
    echo -e "${YELLOW}  ⚠️  Erreur lors de la création de la base${NC}"
    echo -e "${YELLOW}  🔧 Essayez: dotnet ef database drop -f && dotnet ef database update${NC}"
else
    echo -e "${GREEN}  ✅ Base de données créée avec succès${NC}"
fi

# Générer le certificat HTTPS
echo ""
echo -e "${YELLOW}✓ Configuration du certificat HTTPS...${NC}"
dotnet dev-certs https --clean 2>/dev/null
dotnet dev-certs https --trust 2>/dev/null
echo -e "${GREEN}  ✅ Certificat HTTPS configuré${NC}"

# Résumé
echo ""
echo "========================================"
echo -e "${GREEN}✅ CONFIGURATION TERMINÉE${NC}"
echo "========================================"
echo ""
echo -e "${CYAN}📌 Prochaines étapes:${NC}"
echo -e "  1. Ouvrir VSCode: ${YELLOW}code .${NC}"
echo -e "  2. Démarrer l'app: ${YELLOW}dotnet run${NC}"
echo -e "  3. Naviguer vers: ${YELLOW}https://localhost:5001${NC}"
echo ""
echo -e "${CYAN}👤 Identifiants de test:${NC}"
echo -e "  Email: ${YELLOW}admin@novec.fr${NC}"
echo -e "  Mot de passe: ${YELLOW}Test@12345${NC}"
echo ""
echo -e "${CYAN}📖 Documentation: Voir GUIDE_VSCODE.md${NC}"
echo ""
