stxl.modShowInventoryCurrencyTooltip = (priceValue) => {
    const state = priceValue === '' ? 'none' : 'block';
    $('#inventory_sellvalue_box').css('display', state);
    $('#inventory_sellvalue_box #Petal_ValueDisplay').text(priceValue);
};