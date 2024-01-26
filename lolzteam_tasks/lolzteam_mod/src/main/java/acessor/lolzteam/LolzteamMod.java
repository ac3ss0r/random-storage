package acessor.lolzteam;

import net.fabricmc.api.ModInitializer;
import net.fabricmc.fabric.api.item.v1.FabricItemSettings;
import net.fabricmc.fabric.api.object.builder.v1.block.FabricBlockSettings;
import net.minecraft.block.Block;
import net.minecraft.item.BlockItem;
import net.minecraft.registry.Registries;
import net.minecraft.registry.Registry;
import net.minecraft.util.Identifier;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import acessor.lolzteam.mixin.LolzteamBlock;

public class LolzteamMod implements ModInitializer {
	
	public static final Logger LOGGER = LoggerFactory.getLogger("lolzteammod");
    public static final Block EXAMPLE_BLOCK = new LolzteamBlock(FabricBlockSettings.create().strength(2.5f).requiresTool());
    
	@Override
	public void onInitialize() {
        Registry.register(Registries.BLOCK, new Identifier("lolzteammod", "example_block"), EXAMPLE_BLOCK);
        Registry.register(Registries.ITEM, new Identifier("lolzteammod", "example_block"), new BlockItem(EXAMPLE_BLOCK, new FabricItemSettings()));
	}
}