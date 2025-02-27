const testimonials = [
  {
    quote:
      'I’m not a designer, so having pre-built templates that still look professional saved me a ton of time. My portfolio looks amazing now.',
    author: 'John Smith',
    title: 'Full-Stack Developer',
    avatar: 'https://i.pravatar.cc/50',
  },
  {
    quote:
      'I’ve landed more clients since using this tool to create my portfolio. It’s professional, sleek, and user-friendly.',
    author: 'Barack Obama',
    title: 'Freelancer',
    avatar: 'https://i.pravatar.cc/55',
  },
  {
    quote:
      'The platform makes it so simple to create a portfolio that stands out. The support team is fantastic, too!',
    author: 'Emily Brown',
    title: 'UX Designer, StartupX',
    avatar: 'https://i.pravatar.cc/60',
  },
];

export const Testimonials = () => {
  return (
    <section id="testimonials" className="bg-white py-20">
      <div className="container">
        <h2 className="mb-12 text-center text-3xl font-bold">
          What Our Community Says
        </h2>
        <div className="grid grid-cols-1 gap-8 md:grid-cols-3">
          {testimonials.map((testimonial, index) => (
            <div key={index} className="rounded-lg bg-gray-50 p-6 shadow-md">
              <p className="mb-4 text-[15px] text-gray-600">"{testimonial.quote}"</p>
              <div className="flex items-center">
                <img
                  src={testimonial.avatar}
                  alt={testimonial.author}
                  width={60}
                  height={60}
                  className="mr-4 rounded-full"
                />
                <div>
                  <p className="font-semibold">{testimonial.author}</p>
                  <p className="text-sm text-gray-500">{testimonial.title}</p>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
};
