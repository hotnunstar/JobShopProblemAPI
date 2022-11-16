import { AutorServiceService } from './../request/autor-service.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { utilizadores } from '../gestao-utilizadores/gestaoUtilizadoresModel';
import { GestaoUtilizadoresService } from '../request/gestao-utilizadores.service';
import { simulacao } from '../tabela-producao/simulacaoModel';
import { TabelaProducaoService } from '../request/tabela-producao.service';
import { saveAs } from 'file-saver';
import { HomeServiceService } from '../request/home-service.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  utilizador! : any;
  fv:boolean=false;
  newPass!:string;
  oldPass!:string;
  idSimulacao!:number;
  simulacoes:simulacao[]=[];
  df:boolean=false;
  tb:boolean=false;

  constructor(private route:Router, private service: AutorServiceService,private ser: GestaoUtilizadoresService,private serviceUtilizador: GestaoUtilizadoresService,private serv: TabelaProducaoService,private homeService: HomeServiceService  ) { }

  ngOnInit(): void {
    this.carregaSimulacoes();
  }

  logout(): void{
    this.service.discardToken();
    this.route.navigate(['/']);
  }

  ModificaFv()
  {
   
    this.fv=true;
    this.utilizador= this.service.getUtilizadorObj();
    console.log(this.utilizador);
}
alteraPass()
{

  this.ser.insereUtilizador(this.oldPass,this.newPass).subscribe((result)=>{
    if("_response" in result)
    alert(result._response + "\n" + "status Code: "+ result._statusCode);
});
}
inserirDados(){
  this.route.navigate(['/inserirDados']);
}
confirmaUtilizador()
{
  this.serviceUtilizador.retornaUtilizadores().subscribe((result) =>{
    console.log(result)
    if("_response" in result[0])
    {
      alert(result[0]._response);
      this.route.navigate(['/home'])
    }
    
    if("Id" in result[0])
    {
      console.log("entreri")
      this.route.navigate(['/gestaoUtilizadores'])
    }
  });
}
carregaSimulacoes()
{
    this.serv.retornaListaSimulacoes().subscribe((result)=>{
      if("_response" in result)
      {
        
        alert(result._response + "\n" +"status code: " + result._statusCode);
      }else
        this.simulacoes=result;
});
}
validaSimulacao(id:any){
  this.idSimulacao = id;
 

}
modificaDf(){
  
  if(this.df) this.df=false;
  else
  this.df=true;
}
modificaTb(){
  
  if(this.tb) this.tb=false;
  else
  this.tb=true;
}
exportPlano() {
  this.homeService.importarFicheiroPlano(this.idSimulacao).subscribe(data => saveAs(data, `plano_de_producao.txt`));
}
exportTabela() {
  this.homeService.importarFicheiroTabela(this.idSimulacao).subscribe(data => saveAs(data, `tabela_de_producao.txt`));
}
paginaMaquina()
{
  this.route.navigate(['\maquinaEfetouPlano']).then(() => {
    window.location.reload();
  });
}
refresh(): void {
  window.location.reload();
}
}
