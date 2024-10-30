import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/apiService/api.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-pessoas',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './pessoas.component.html',
  styleUrls: ['./pessoas.component.css'],
})
export class PessoasComponent implements OnInit {
  pessoas: any[] = [];
  blank: any = {
    nome: '',
    email: '',
    dataNascimento: '',
    status: 'disponível',
  };
  novaPessoa: any;
  buttonName: string = 'Adicionar';

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.apiService.getPessoas().subscribe((data) => {
      this.pessoas = data;
    });
    this.novaPessoa = this.blank;
  }

  definirDados(dados: any) {
    this.novaPessoa = dados;
    this.buttonName = 'Salvar';
  }

  checkAddorUpdate() {
    if (this.buttonName == 'Salvar') {
      this.updatePessoa(this.novaPessoa.pessoaId, this.novaPessoa);
    } else {
      this.addPessoa();
    }
  }

  addPessoa() {
    this.apiService.addPessoa(this.novaPessoa).subscribe({
      next: (data) => {
        this.pessoas.push(data);
        alert('Pessoa salva com sucesso!');
        this.novaPessoa = { nome: '', email: '', dataNascimento: '' };
      },
      error: (err) => {
        const errorMessage = err.error.message;
        alert(errorMessage);
      },
    });
  }

  deletePessoa(pessoaId: number) {
    this.apiService.deletePessoa(pessoaId).subscribe({
      next: () => {
        this.pessoas = this.pessoas.filter(
          (Pessoa) => Pessoa.pessoaId !== pessoaId
        );
        window.location.reload();
      },
      error: (err) => {
        const errorMessage = err.error.message;
        alert(errorMessage);
      },
    });
  }

  updatePessoa(id: number, pessoa: any) {
    this.apiService.updatePessoa(id, pessoa).subscribe({
      next: () => {
        this.pessoas = this.pessoas.filter((pessoa) => pessoa.id !== id);
        alert('Pessoa atualizada com sucesso!');
        this.novaPessoa = this.blank;
      },
      error: (err) => {
        const errorMessage = err.error.message;
        alert(errorMessage);
      },
    });
  }
}
