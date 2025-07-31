import { Link } from 'react-router-dom';
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { BookOpen, Users, Tag, Plus } from 'lucide-react';

export default function Home() {
  const cards = [
    {
      title: 'Autores',
      description: 'Gerencie os autores dos livros',
      icon: Users,
      path: '/autores',
      color: 'bg-green-500',
    },
    {
      title: 'Gêneros',
      description: 'Organize os gêneros literários',
      icon: Tag,
      path: '/generos',
      color: 'bg-purple-500',
    },
    {
      title: 'Livros',
      description: 'Cadastre e gerencie livros',
      icon: BookOpen,
      path: '/livros',
      color: 'bg-blue-500',
    },
  ];

  return (
    <div className="space-y-6">
      <div className="text-center">
        <h1 className="text-3xl font-bold text-gray-900 mb-2">
          Sistema de Gerenciamento de Biblioteca
        </h1>
        <p className="text-lg text-gray-600">
          Gerencie autores, gêneros e livros de forma simples e eficiente
        </p>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mt-8">
        {cards.map(({ title, description, icon: Icon, path, color }) => (
          <Card key={title} className="hover:shadow-lg transition-shadow">
            <CardHeader className="text-center">
              <div className={`w-16 h-16 ${color} rounded-full flex items-center justify-center mx-auto mb-4`}>
                <Icon className="h-8 w-8 text-white" />
              </div>
              <CardTitle className="text-xl">{title}</CardTitle>
              <CardDescription>{description}</CardDescription>
            </CardHeader>
            <CardContent className="text-center">
              <Link to={path}>
                <Button className="w-full">
                  <Plus className="h-4 w-4 mr-2" />
                  Gerenciar {title}
                </Button>
              </Link>
            </CardContent>
          </Card>
        ))}
      </div>

      <div className="bg-white rounded-lg shadow p-6 mt-8">
        <h2 className="text-xl font-semibold mb-4">Funcionalidades</h2>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div className="flex items-start space-x-3">
            <div className="w-2 h-2 bg-blue-500 rounded-full mt-2"></div>
            <div>
              <h3 className="font-medium">CRUD Completo</h3>
              <p className="text-gray-600 text-sm">Criar, visualizar, editar e excluir registros</p>
            </div>
          </div>
          <div className="flex items-start space-x-3">
            <div className="w-2 h-2 bg-green-500 rounded-full mt-2"></div>
            <div>
              <h3 className="font-medium">Interface Intuitiva</h3>
              <p className="text-gray-600 text-sm">Design moderno e fácil de usar</p>
            </div>
          </div>
          <div className="flex items-start space-x-3">
            <div className="w-2 h-2 bg-purple-500 rounded-full mt-2"></div>
            <div>
              <h3 className="font-medium">Relacionamentos</h3>
              <p className="text-gray-600 text-sm">Livros conectados a autores e gêneros</p>
            </div>
          </div>
          <div className="flex items-start space-x-3">
            <div className="w-2 h-2 bg-orange-500 rounded-full mt-2"></div>
            <div>
              <h3 className="font-medium">Validação</h3>
              <p className="text-gray-600 text-sm">Formulários com validação de dados</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

