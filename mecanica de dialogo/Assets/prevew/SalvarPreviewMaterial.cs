using UnityEngine;
using UnityEditor;
using System.IO;

public class SalvarPreviewMaterial
{
    [MenuItem("Assets/Converter Preview do Material em Sprite", false, 10)]
    private static void ConverterMaterialParaSprite()
    {
        Material mat = Selection.activeObject as Material;

        if (mat == null)
        {
            Debug.LogError("Selecione um arquivo de Material primeiro!");
            return;
        }

        // Pega a imagem exata do preview do Material
        Texture2D texturePreview = AssetPreview.GetAssetPreview(mat);

        if (texturePreview == null)
        {
            Debug.LogWarning("Aguarde a Unity carregar o preview do Material e tente novamente.");
            return;
        }

        // Converte em arquivo PNG
        byte[] bytes = texturePreview.EncodeToPNG();
        string caminho = AssetDatabase.GetAssetPath(mat);
        string caminhoSalvar = caminho.Replace(".mat", "_Sprite.png");

        File.WriteAllBytes(caminhoSalvar, bytes);
        AssetDatabase.Refresh();

        // Configura a imagem recém-criada para ser um Sprite
        TextureImporter importer = AssetImporter.GetAtPath(caminhoSalvar) as TextureImporter;
        if (importer != null)
        {
            importer.textureType = TextureImporterType.Sprite;
            importer.SaveAndReimport();
        }

        Debug.Log($"<color=green>Sprite criado com sucesso em: {caminhoSalvar}</color>");
    }
}