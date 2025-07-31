import { useState, useEffect } from 'react';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogTrigger } from '@/components/ui/dialog';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table';
import { Plus, Edit, Trash2, BookOpen } from 'lucide-react';
import { livroService, autorService, generoService } from '@/lib/services';

export default function Livros() {
  const [livros, setLivros] = useState([]);
  const [autores, setAutores] = useState([]);
  const [generos, setGeneros] = useState([]);
  const [loading, setLoading] = useState(true);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [editingLivro, setEditingLivro] = useState(null);
  const [formData, setFormData] = useState({
    titulo: '',
    autorId: '',
    generoId: '',
  });

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      setLoading(true);
      const [livrosResponse, autoresResponse, generosResponse] = await Promise.all([
        livroService.getAll(),
        autorService.getAll(),
        generoService.getAll(),
      ]);
      setLivros(livrosResponse.data);
      setAutores(autoresResponse.data);
      setGeneros(generosResponse.data);
    } catch (error) {
      console.error('Erro ao carregar dados:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const data = {
        titulo: formData.titulo,
        autorId: parseInt(formData.autorId),
        generoId: parseInt(formData.generoId),
      };

      if (editingLivro) {
        await livroService.update(editingLivro.id, { ...data, id: editingLivro.id });
      } else {
        await livroService.create(data);
      }
      setDialogOpen(false);
      setFormData({ titulo: '', autorId: '', generoId: '' });
      setEditingLivro(null);
      loadData();
    } catch (error) {
      console.error('Erro ao salvar livro:', error);
    }
  };

  const handleEdit = (livro) => {
    setEditingLivro(livro);
    setFormData({
      titulo: livro.titulo,
      autorId: livro.autorId.toString(),
      generoId: livro.generoId.toString(),
    });
    setDialogOpen(true);
  };

  const handleDelete = async (id) => {
    if (window.confirm('Tem certeza que deseja excluir este livro?')) {
      try {
        await livroService.delete(id);
        loadData();
      } catch (error) {
        console.error('Erro ao excluir livro:', error);
      }
    }
  };

  const resetForm = () => {
    setFormData({ titulo: '', autorId: '', generoId: '' });
    setEditingLivro(null);
  };

  const getAutorNome = (autorId) => {
    const autor = autores.find(a => a.id === autorId);
    return autor ? autor.nome : 'N/A';
  };

  const getGeneroNome = (generoId) => {
    const genero = generos.find(g => g.id === generoId);
    return genero ? genero.nome : 'N/A';
  };

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <div className="flex items-center space-x-3">
          <BookOpen className="h-8 w-8 text-blue-600" />
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Livros</h1>
            <p className="text-gray-600">Gerencie o catálogo de livros</p>
          </div>
        </div>
        <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
          <DialogTrigger asChild>
            <Button onClick={resetForm}>
              <Plus className="h-4 w-4 mr-2" />
              Novo Livro
            </Button>
          </DialogTrigger>
          <DialogContent className="sm:max-w-[425px]">
            <DialogHeader>
              <DialogTitle>
                {editingLivro ? 'Editar Livro' : 'Novo Livro'}
              </DialogTitle>
            </DialogHeader>
            <form onSubmit={handleSubmit} className="space-y-4">
              <div>
                <Label htmlFor="titulo">Título</Label>
                <Input
                  id="titulo"
                  value={formData.titulo}
                  onChange={(e) => setFormData({ ...formData, titulo: e.target.value })}
                  placeholder="Digite o título do livro"
                  required
                />
              </div>
              <div>
                <Label htmlFor="autor">Autor</Label>
                <Select
                  value={formData.autorId}
                  onValueChange={(value) => setFormData({ ...formData, autorId: value })}
                  required
                >
                  <SelectTrigger>
                    <SelectValue placeholder="Selecione um autor" />
                  </SelectTrigger>
                  <SelectContent>
                    {autores.map((autor) => (
                      <SelectItem key={autor.id} value={autor.id.toString()}>
                        {autor.nome}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>
              <div>
                <Label htmlFor="genero">Gênero</Label>
                <Select
                  value={formData.generoId}
                  onValueChange={(value) => setFormData({ ...formData, generoId: value })}
                  required
                >
                  <SelectTrigger>
                    <SelectValue placeholder="Selecione um gênero" />
                  </SelectTrigger>
                  <SelectContent>
                    {generos.map((genero) => (
                      <SelectItem key={genero.id} value={genero.id.toString()}>
                        {genero.nome}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>
              <div className="flex justify-end space-x-2">
                <Button
                  type="button"
                  variant="outline"
                  onClick={() => setDialogOpen(false)}
                >
                  Cancelar
                </Button>
                <Button type="submit">
                  {editingLivro ? 'Atualizar' : 'Criar'}
                </Button>
              </div>
            </form>
          </DialogContent>
        </Dialog>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Lista de Livros</CardTitle>
        </CardHeader>
        <CardContent>
          {loading ? (
            <div className="text-center py-4">Carregando...</div>
          ) : (
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead>ID</TableHead>
                  <TableHead>Título</TableHead>
                  <TableHead>Autor</TableHead>
                  <TableHead>Gênero</TableHead>
                  <TableHead className="text-right">Ações</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {livros.length === 0 ? (
                  <TableRow>
                    <TableCell colSpan={5} className="text-center py-4 text-gray-500">
                      Nenhum livro encontrado
                    </TableCell>
                  </TableRow>
                ) : (
                  livros.map((livro) => (
                    <TableRow key={livro.id}>
                      <TableCell>{livro.id}</TableCell>
                      <TableCell className="font-medium">{livro.titulo}</TableCell>
                      <TableCell>{getAutorNome(livro.autorId)}</TableCell>
                      <TableCell>{getGeneroNome(livro.generoId)}</TableCell>
                      <TableCell className="text-right">
                        <div className="flex justify-end space-x-2">
                          <Button
                            variant="outline"
                            size="sm"
                            onClick={() => handleEdit(livro)}
                          >
                            <Edit className="h-4 w-4" />
                          </Button>
                          <Button
                            variant="outline"
                            size="sm"
                            onClick={() => handleDelete(livro.id)}
                            className="text-red-600 hover:text-red-700"
                          >
                            <Trash2 className="h-4 w-4" />
                          </Button>
                        </div>
                      </TableCell>
                    </TableRow>
                  ))
                )}
              </TableBody>
            </Table>
          )}
        </CardContent>
      </Card>
    </div>
  );
}

