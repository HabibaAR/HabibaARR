#!/bin/bash

echo "════════════════════════════════════════════════════════════"
echo "🚀  DÉMARRAGE DE L'APPLICATION GPA NOVEC"
echo "════════════════════════════════════════════════════════════"
echo ""

# Couleurs
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

echo -e "${BLUE}📋 Vérification des prérequis...${NC}"
echo ""

# Vérifier .NET SDK
if ! command -v dotnet &> /dev/null; then
    echo -e "${YELLOW}❌ .NET SDK non trouvé!${NC}"
    echo "Téléchargez .NET 8.0 depuis: https://dotnet.microsoft.com/download"
    exit 1
fi

DOTNET_VERSION=$(dotnet --version)
echo -e "${GREEN}✅ .NET SDK installé: $DOTNET_VERSION${NC}"
echo ""

# Étape 1: Restaurer les dépendances
echo -e "${BLUE}1️⃣  Restauration des dépendances NuGet...${NC}"
dotnet restore
if [ $? -ne 0 ]; then
    echo -e "${YELLOW}❌ Erreur lors de la restauration${NC}"
    exit 1
fi
echo -e "${GREEN}✅ Dépendances restaurées${NC}"
echo ""

# Étape 2: Mettre à jour la base de données
echo -e "${BLUE}2️⃣  Mise à jour de la base de données...${NC}"
dotnet ef database update
if [ $? -ne 0 ]; then
    echo -e "${YELLOW}⚠️  Attention: La base de données n'a pas pu être mise à jour${NC}"
    echo "   Essayez: dotnet ef database drop -f && dotnet ef database update"
fi
echo -e "${GREEN}✅ Base de données prête${NC}"
echo ""

# Étape 3: Compiler l'application
echo -e "${BLUE}3️⃣  Compilation de l'application...${NC}"
dotnet build
if [ $? -ne 0 ]; then
    echo -e "${YELLOW}❌ Erreur lors de la compilation${NC}"
    exit 1
fi
echo -e "${GREEN}✅ Application compilée${NC}"
echo ""

# Étape 4: Lancer l'application
echo -e "${BLUE}4️⃣  Lancement de l'application...${NC}"
echo ""
echo "════════════════════════════════════════════════════════════"
echo -e "${GREEN}🟢 L'APPLICATION DÉMARRE!${NC}"
echo "════════════════════════════════════════════════════════════"
echo ""
echo -e "${YELLOW}📱 Accès:${NC}"
echo "   🔗 https://localhost:5001"
echo ""
echo -e "${YELLOW}🔑 Identifiants de test:${NC}"
echo "   👤 Email: gestionnaire@novec.fr"
echo "   🔐 Mot de passe: Test@12345"
echo ""
echo -e "${YELLOW}Autres utilisateurs:${NC}"
echo "   - responsable@novec.fr (Test@12345)"
echo "   - directeur@novec.fr (Test@12345)"
echo "   - admin@novec.fr (Test@12345)"
echo ""
echo "════════════════════════════════════════════════════════════"
echo ""

dotnet run

echo ""
echo "════════════════════════════════════════════════════════════"
echo "🛑 Application terminée"
echo "════════════════════════════════════════════════════════════"
