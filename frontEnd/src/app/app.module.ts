import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { TabelaProducaoComponent } from './tabela-producao/tabela-producao.component';
import { GestaoUtilizadoresComponent } from './gestao-utilizadores/gestao-utilizadores.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AtribuirTemposComponent } from './atribuir-tempos/atribuir-tempos.component';
import { InterceptorTokenService } from './request/interceptor-token.service';
import { InserirDadosComponent } from './inserir-dados/inserir-dados.component';
import { MaquinaEfetouPlanoComponent } from './maquina-efetou-plano/maquina-efetou-plano.component';
import { ListaSimulacoesComponent } from './lista-simulacoes/lista-simulacoes.component';
import { ApagarSimulacoesComponent } from './apagar-simulacoes/apagar-simulacoes.component';
import { TempoAutomaticoComponent } from './tempo-automatico/tempo-automatico.component';
import { AlterarTabelaComponent } from './alterar-tabela/alterar-tabela.component';


@NgModule({
  declarations: [
    
    AppComponent,
    LoginComponent,
    HomeComponent,
    TabelaProducaoComponent,
    GestaoUtilizadoresComponent,
    AtribuirTemposComponent,
    InserirDadosComponent,
    MaquinaEfetouPlanoComponent,
    ListaSimulacoesComponent,
    ApagarSimulacoesComponent,
    TempoAutomaticoComponent,
    AlterarTabelaComponent,
    
   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: InterceptorTokenService, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
