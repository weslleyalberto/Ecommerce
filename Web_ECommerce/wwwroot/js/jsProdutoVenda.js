var ObjetoVenda = new Object();

ObjetoVenda.AdicionarCarrinho = function (idProduto) {



    var nome = $("#nome_" + idProduto).val();
    var qtd = $("#qtd_" + idProduto).val();


    $.ajax({
        type: 'POST',
        url: '/api/AdicionarProdutoCarrinho',
        dataType: 'JSON',
        cache: false,
        async: true,
        data: {
            "id": idProduto, "nome": nome, "qtd": qtd
        },
        success: function (data) {
            if (data.sucesso) {
                // 1 -alert-sucess 2 - alert-warning  3- alert-danger
                ObjetoAlerta.AlertarTela(1, "Produto Adicionado no carrinho!");
            }
            else {
                ObjetoAlerta.AlertarTela(2, "Necessário efetuar o login");
            }
        }
    })
};


ObjetoVenda.CarregaProdutos = function () {
    $.ajax({
        type: 'GET',
        url: '/api/ListaProdutoComEstoque',
        dataType: 'JSON',
        cache: false,
        async: true,
        success: function (data) {
            var htmlConteudo = "";

            data.forEach(function (entitie) {
                htmlConteudo += "<div class='col-xs-12 cold-sm-4 col-lg-4>'";

                var idNome = "nome_" + entitie.id;
                var idQtd = "qtd_" + entitie.id;
                htmlConteudo += "<label id='" + idNome + "'> Produtos: " + entitie.nome + "</label><br/>";
                htmlConteudo += "<label> Valor: " + entitie.valor + "</label></br>";
                htmlConteudo += "Quantidade : <input type='number' value='1' id='" + idQtd + "'>";
                htmlConteudo += "<input type='button' onclick='ObjetoVenda.AdicionarCarrinho(" + entitie.id+")' value='Comprar'></br>";
                htmlConteudo += "</div>";
            });
            $("#DivVenda").html(htmlConteudo);
        }
    });
};

ObjetoVenda.CarregaQtdCarrinho = function () {
    $("#qtdCarrinho").text("(1000)");
    setTimeout(ObjetoVenda.CarregaQtdCarrinho, 10000);
}
$(function () {
    ObjetoVenda.CarregaProdutos();
    ObjetoVenda.CarregaQtdCarrinho();
});
